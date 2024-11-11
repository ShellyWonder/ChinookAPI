using ChinookInterviewYT.Client.Models;
using ChinookInterviewYT.Client.Models.DTOs;
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
        
        //Customers + paging
        public async Task<List<Customer>> GetAllCustomersAsync(int pageNumber, int pageSize)
        {
            return await _context.Customers
                                .OrderBy(c => c.LastName)
                                //pagination: fetch only customers needed for requested page.
                                .Skip((pageNumber - 1) * pageSize)
                                .Take(pageSize)
                                .ToListAsync();
        }

        //Customers + paging + filter by customer Id
        public async Task<PagedResultDTO<CustomerInvoiceDTO>> GetAllCustomersInvoicesAsync(int pageNumber, int pageSize, int customerId)
        {
            IQueryable<Customer> query = _context.Customers.OrderBy(c => c.LastName);

            if (customerId > 0) query = query.Where(c => c.CustomerId == customerId);

            int TotalRecords =  await query.CountAsync();
            var customers = await query.Skip((pageNumber - 1) * pageSize)
                                         .Take(pageSize)
                                         .Select(c => new CustomerInvoiceDTO
                                         {
                                             CustomerId = c.CustomerId,
                                             FirstName = c.FirstName,
                                             LastName = c.LastName,
                                             Email = c.Email,
                                             Invoices = c.Invoices.Select(i => new InvoiceDTO
                                             {
                                                 InvoiceId = i.InvoiceId,
                                                 InvoiceDate = i.InvoiceDate,
                                                 InvoiceTotal = i.Total,
                                                 LineItems = i.InvoiceLines.Select(il => new InvoiceLineDTO
                                                 {
                                                    InvoiceLineId = il.InvoiceLineId,
                                                    TrackId = il.InvoiceLineId,
                                                    UnitPrice = il.UnitPrice,
                                                    Quantity = il.Quantity,
                                                 }).ToList()
                                             }).ToList(),
                                         }).ToListAsync();
            return new PagedResultDTO<CustomerInvoiceDTO>
            {
                Results = customers,
                TotalCount = TotalRecords
            };
        }
        public async Task<int> GetTotalCustomersAsync()
        {
             return await _context.Customers.CountAsync();
        }
        public async Task<List<CustomerDTO>> GetAllCustomersDTOAsync()
        {
            return await _context.Customers
                                .OrderBy(c => c.LastName)
                                .Select(c => new CustomerDTO
                                {
                                    CustomerId = c.CustomerId,
                                    FirstName = c.FirstName,
                                    LastName = c.LastName,
                                    Company = c.Company,
                                    Address = c.Address,
                                    City = c.City,
                                    State = c.State,
                                    Country = c.Country,
                                    PostalCode = c.PostalCode,
                                    Phone = c.Phone,
                                    Email = c.Email
                                })
                                .ToListAsync();
        }
    }
}
