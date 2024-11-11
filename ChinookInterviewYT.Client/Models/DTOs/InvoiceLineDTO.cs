namespace ChinookInterviewYT.Client.Models.DTOs
{
    public class InvoiceLineDTO
    {
        public int InvoiceLineId { get; set; }
        public int InvoiceId { get; set; }
        public int TrackId { get; set; }
        public string? TrackTitle { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SubTotal => Quantity * UnitPrice;
    }
}
