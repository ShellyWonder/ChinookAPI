using ChinookInterviewYT.Client.Models;
using ChinookInterviewYT.Client.Models.DTOs;

namespace ChinookInterviewYT.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<(List<Customer> Customers, int TotalCount)> GetPagedCustomersAsync(int pageNumber, int pageSize);
        Task<PagedResultDTO<CustomerInvoiceDTO>> GetAllCustomersInvoicesAsync(int pageNumber, int pageSize, int customerId);
        Task<List<CustomerDTO>> GetAllCustomersDTOAsync();
        Task<List<CustomerSpendingDTO>> GetAllCustomersSpendingAsync();
    }
}
