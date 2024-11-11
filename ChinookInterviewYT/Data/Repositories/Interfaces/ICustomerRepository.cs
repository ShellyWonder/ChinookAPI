using ChinookInterviewYT.Client.Models;
using ChinookInterviewYT.Client.Models.DTOs;

namespace ChinookInterviewYT.Data.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        // customer only
        Task<int> GetTotalCustomersAsync();

        // customer + pagination 
        Task<List<Customer>>GetAllCustomersAsync(int pageNumber, int pageSize);

        // customer + pagination + customerId
        Task<PagedResultDTO<CustomerInvoiceDTO>> GetAllCustomersInvoicesAsync(int pageNumber, int pageSize, int customerId);
        Task<List<CustomerDTO>> GetAllCustomersDTOAsync();
    }
}
