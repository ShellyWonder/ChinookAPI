using ChinookInterviewYT.Client.Models;

namespace ChinookInterviewYT.Data.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Task<int> GetTotalCustomersAsync();
        Task <List<Customer>>GetAllCustomersAsync(int pageNumber, int pageSize);
    }
}
