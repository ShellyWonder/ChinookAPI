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

        #region GET CUSTOMERS VIA CUSTOMER DTO
        [HttpGet("GetAllCustomersDTO")]
        public async Task<ActionResult<CustomerDTO>> GetAllCustomersDTOAsync()
        {
            try
            {
                var customers = await _customerService.GetAllCustomersDTOAsync();

                if (customers == null) customers = new List<CustomerDTO>();
                return Ok(customers);
            }

	        catch (Exception ex)
	        {

		        Console.WriteLine(ex.Message);
                return Problem();
	        }
        }
        #endregion

        #region SEARCH INDEX
        [HttpGet("search")]
        public async Task<ActionResult<PagedResultDTO<Customer>>> SearchIndex(int? page, string searchTerm)
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
            var searchResult = new PagedResultDTO<Customer>
            {
                Results = customers,
                TotalCount = totalCount
            };

            return Ok(searchResult);
        }
        #endregion

        #region GET INVOICES WITH LINE ITEMS
        [HttpGet("GetInvoicesWithLineItems")]
        public async Task<ActionResult<PagedCustomerInvoiceDTO>>GetInvoicesWithLineItems([FromQuery]int pageNumber = 1, int pageSize = 10, int customerId = 0)
        {
            try
            {
                if (pageNumber < 1) pageNumber = 1;
                if (pageSize < 1 || pageSize> 25) pageSize = 10;

                var customers = await _customerService.GetAllCustomersInvoicesAsync(pageNumber, pageSize, customerId);
                 return Ok(customers);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return Problem();
            }
        }
        #endregion

        #region CUSTOMER SPENDING
        [HttpGet("CustomerSpending")]
        public async Task<ActionResult<CustomerSpendingDTO>>GetCustomerSpendingAsync()
        {
            try
            {
                var result = await _customerService.GetAllCustomersSpendingAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {

                Console.Write(ex.Message);
                return Problem();   
            }
           
        }

        #endregion


    }
}
