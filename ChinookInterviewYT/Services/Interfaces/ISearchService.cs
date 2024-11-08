using ChinookInterviewYT.Client.Models;

namespace ChinookInterviewYT.Services.Interfaces
{
    public interface ISearchService
    {
        Task<(List<Customer> Customers, int TotalCount)> SearchCustomersAsync(int pageNumber, int pageSize, string searchTerm);
    }
}
