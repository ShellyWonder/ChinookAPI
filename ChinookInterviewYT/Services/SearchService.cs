using ChinookInterviewYT.Client.Models;
using ChinookInterviewYT.Data.Repositories;
using ChinookInterviewYT.Data.Repositories.Interfaces;
using ChinookInterviewYT.Services.Interfaces;
using Microsoft.Extensions.Hosting;

namespace ChinookInterviewYT.Services
{
    public class SearchService : ISearchService
    {
        private readonly ISearchRepository _searchRepository;

        public SearchService(ISearchRepository searchRepository)
        {
            _searchRepository = searchRepository;
        }

        #region SEARCH CUSTOMERS
        public async Task<(List<Customer> Customers, int TotalCount)> SearchCustomersAsync(int pageNumber, int pageSize, string searchTerm)
        {
            // Fetch the paged data and the total count from the repository.
            var customers = await _searchRepository.SearchCustomersAsync( pageNumber, pageSize, searchTerm);
            var totalCount = await _searchRepository.GetTotalCountAsync( searchTerm);

            return (customers, totalCount);
        }
        #endregion
    }
}
