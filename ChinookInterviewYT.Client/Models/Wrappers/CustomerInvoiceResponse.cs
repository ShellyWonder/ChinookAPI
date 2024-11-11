using ChinookInterviewYT.Client.Models.DTOs;

namespace ChinookInterviewYT.Client.Models.Wrappers
{
    public class CustomerInvoiceResponse
    {
        public object? Result { get; set; } // Expecting this to remain null or unused
        public CustomerInvoiceResponse? Value { get; set; }  // Contains the paginated customer invoice data
    }

}
