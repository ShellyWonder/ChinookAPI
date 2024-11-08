using ChinookInterviewYT.Client.Models;

namespace ChinookInterviewYT.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<(List<Customer> Customers, int TotalCount)> GetPagedCustomersAsync(int pageNumber, int pageSize);
    }
}
