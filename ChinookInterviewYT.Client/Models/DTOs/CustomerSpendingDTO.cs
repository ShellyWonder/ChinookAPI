namespace ChinookInterviewYT.Client.Models.DTOs
{
    public class CustomerSpendingDTO
    {
        public int CustomerId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public int TotalInvoices { get; set; }
        public decimal TotalAmountSpent { get; set; }
        public decimal PercentageOfTotalRevenue { get; set; }
    }
}
