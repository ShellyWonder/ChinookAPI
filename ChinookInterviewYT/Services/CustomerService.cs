using ChinookInterviewYT.Client.Models;
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
    }
}
