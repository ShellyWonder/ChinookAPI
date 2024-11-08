using ChinookInterviewYT.Client.Models;
using ChinookInterviewYT.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChinookInterviewYT.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ChinookDbContext _context;

        public CustomerRepository(ChinookDbContext context)
        {
            _context = context;
        }

        public async Task<List<Customer>> GetAllCustomersAsync(int pageNumber, int pageSize)
        {
            return await _context.Customers
                                .OrderBy(c => c.LastName)
                                //pagination: fetch only customers needed for requested page.
                                .Skip((pageNumber - 1) * pageSize)
                                .Take(pageSize)
                                .ToListAsync();
        }

        public async Task<int> GetTotalCustomersAsync()
        {
             return await _context.Customers.CountAsync();
        }
    }
}
