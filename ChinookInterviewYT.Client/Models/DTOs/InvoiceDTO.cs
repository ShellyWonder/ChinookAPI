namespace ChinookInterviewYT.Client.Models.DTOs
{
    public class InvoiceDTO
    {
        public int InvoiceId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string? BillingAddress { get; set; }
        public string? BillingCity { get; set; }
        public string? BillingState { get; set; }
        public string? BillingPostalCode { get; set; }
        public decimal? InvoiceTotal { get; set; }

        public List<InvoiceLineDTO> LineItems { get; set; } = new();
    }
}
