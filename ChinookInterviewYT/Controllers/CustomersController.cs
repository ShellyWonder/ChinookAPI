using ChinookInterviewYT.Client.Models;
using ChinookInterviewYT.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ChinookInterviewYT.Client.Models.DTOs;

namespace ChinookInterviewYT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController(ICustomerService customerService,
                                      ISearchService searchService) : ControllerBase
    {
        
        private readonly ICustomerService _customerService = customerService;
        private readonly ISearchService _searchService = searchService;

        #region GET CUSTOMERS
        [HttpGet("GetPagedCustomers")]
        public async Task<ActionResult<PagedCustomerDTO>> GetPagedCustomersAsync([FromQuery] int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var (customers, totalCount) = await _customerService.GetPagedCustomersAsync(pageNumber, pageSize);
                var pagedResult = new PagedCustomerDTO
                {
                    Customers = customers,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalCount = totalCount
                };

                return Ok(pagedResult);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound();
            }
        }
        #endregion

        #region SEARCH INDEX
        [HttpGet("search")]
        public async Task<ActionResult<SearchResultDTO<Customer>>> SearchIndex(int? page, string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return NotFound($"The search term '{searchTerm}' was not found. Please try again.");
            }

            //pagination variables
            var pageNumber = page ?? 1;
            var pageSize = 5;

            // Unpack the tuple from the service
            var (customers, totalCount) = await _searchService.SearchCustomersAsync(pageNumber, pageSize, searchTerm);

            // Create the SearchResultDTO
            var searchResult = new SearchResultDTO<Customer>
            {
                Results = customers,
                TotalCount = totalCount
            };

            return Ok(searchResult);
        }
        #endregion
    }
}
