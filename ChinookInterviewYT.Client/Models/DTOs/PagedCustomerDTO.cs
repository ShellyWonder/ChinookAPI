namespace ChinookInterviewYT.Client.Models.DTOs
{
    public class PagedCustomerDTO
    {
        public List<Customer> Customers { get; set; } = new List<Customer>();
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalCount { get; set; } = 0;
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    }
}
