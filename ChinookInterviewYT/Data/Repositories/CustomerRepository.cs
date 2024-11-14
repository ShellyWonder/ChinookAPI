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

     #region CONSTRUCTOR
        public CustomerRepository(ChinookDbContext context)
        {
            _context = context;
        } 
        #endregion

     #region GET ALL CUSTOMERS + PAGING
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
        #endregion

     #region GET CUSTOMER INVOICES
        //Customers + paging + filter by customer Id
        public async Task<PagedResultDTO<CustomerInvoiceDTO>> GetAllCustomersInvoicesAsync(int pageNumber, int pageSize, int customerId)
        {
            IQueryable<Customer> query = _context.Customers.OrderBy(c => c.LastName);

            if (customerId > 0) query = query.Where(c => c.CustomerId == customerId);

            int TotalRecords = await query.CountAsync();
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
                                                     TrackTitle = il.Track.Name,
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
        #endregion

     #region GET TOTAL CUSTOMER COUNT
        public async Task<int> GetTotalCustomersAsync()
        {
            return await _context.Customers.CountAsync();
        }
        #endregion

     #region GET ALL CUSTOMERS VIA CUSTOMER DTO 
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
        #endregion

     #region CUSTOMER SPENDING OVER $40
        public async Task<List<CustomerSpendingDTO>> GetAllCustomersSpendingAsync()
        {
            decimal TotalStoreRevenue = await _context.Invoices.SumAsync(i => i.Total);
            var result = await _context.Customers
                                                           .Where(c => c.Invoices.Sum(i => i.Total) > 40)
                                                           .Select(c => new CustomerSpendingDTO
                                                           {
                                                               CustomerId = c.CustomerId,
                                                               FullName = c.FirstName + " " + c.LastName,
                                                               TotalInvoices = c.Invoices.Count(),
                                                               TotalAmountSpent = c.Invoices.Sum(i => i.Total),
                                                               PercentageOfTotalRevenue = c.Invoices.Sum(i => i.Total) / TotalStoreRevenue,
                                                           }).OrderByDescending(c => c.TotalAmountSpent)
                                                           .ToListAsync();
            return result;
        } 
        #endregion
    }
}
