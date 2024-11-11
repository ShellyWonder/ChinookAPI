using System.Text.Json.Serialization;

namespace ChinookInterviewYT.Client.Models.DTOs
{
    public class PagedCustomerInvoiceDTO
    {
        public List<CustomerInvoiceDTO> Customers { get; set; } = new();
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        [JsonPropertyName("totalCount")] // Maps JSON "totalCount" to this property
        public int TotalCount { get; set; } = 0;
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    }
}
