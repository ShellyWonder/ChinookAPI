using ChinookInterviewYT.Client.Models;
using ChinookInterviewYT.Client.Models.DTOs;
using ChinookInterviewYT.Data.Repositories.Interfaces;
using ChinookInterviewYT.Services.Interfaces;

namespace ChinookInterviewYT.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<(List<Customer> Customers, int TotalCount)>GetPagedCustomersAsync(int pageNumber, int pageSize)
        {
            var customers = await _customerRepository.GetAllCustomersAsync(pageNumber, pageSize);
            var totalCount = await _customerRepository.GetTotalCustomersAsync();
            return (customers, totalCount);
        }

        public async Task<List<CustomerDTO>> GetAllCustomersDTOAsync()
        {
            return  await _customerRepository.GetAllCustomersDTOAsync();
        }

        public async Task<PagedResultDTO<CustomerInvoiceDTO>> GetAllCustomersInvoicesAsync(int pageNumber, int pageSize, int customerId)
        {
            return await _customerRepository.GetAllCustomersInvoicesAsync(pageNumber, pageSize, customerId);
        }
    }
}
