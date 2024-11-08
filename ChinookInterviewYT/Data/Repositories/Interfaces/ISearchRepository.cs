using ChinookInterviewYT.Client.Models;

namespace ChinookInterviewYT.Data.Repositories.Interfaces
{
    public interface ISearchRepository
    {
        Task<List<Customer>> SearchCustomersAsync(int pageNumber, int pageSize, string searchTerm);
        Task<int> GetTotalCountAsync(string searchTerm);
    }
}
