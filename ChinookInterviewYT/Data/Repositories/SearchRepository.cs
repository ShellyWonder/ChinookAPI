using ChinookInterviewYT.Client.Models;
using ChinookInterviewYT.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace ChinookInterviewYT.Data.Repositories
{
    public class SearchRepository : ISearchRepository
    {
        private readonly ChinookDbContext _context;

        public SearchRepository(ChinookDbContext context)
        {
            _context = context;
        }

        public async Task<List<Customer>> SearchCustomersAsync(int pageNumber, int pageSize, string searchTerm)
        {
            IQueryable<Customer> customers = _context.Customers;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower();

                customers = customers.Where(c =>
                        c.FirstName.ToLower().Contains(searchTerm) ||
                        c.LastName.ToLower().Contains(searchTerm) ||
                        c.Email.ToLower().Contains(searchTerm)
                 );
            }

            return await customers.OrderBy(c => c.LastName)
                                  .ThenBy(c => c.FirstName)
                                  .Skip((pageNumber - 1) * pageSize)
                                  .Take(pageSize)
                                  .ToListAsync();
        }
        public async Task<int> GetTotalCountAsync(string searchTerm)
        {
            IQueryable<Customer> customers = _context.Customers;
            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower();

                customers = customers.Where(c =>
                c.FirstName.ToLower().Contains(searchTerm) ||
                (c.LastName != null && c.LastName.ToLower().Contains(searchTerm)) ||
                (c.Email != null && c.Email.ToLower().Contains(searchTerm))
                );
            }

            // Return the count of the filtered results
            return await customers.CountAsync();
        }
    }
}
