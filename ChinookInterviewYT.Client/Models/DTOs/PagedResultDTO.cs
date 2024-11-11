namespace ChinookInterviewYT.Client.Models.DTOs
{
    public class PagedResultDTO<T>
    {
        public List<T>? Results { get; set; }
        public int TotalCount { get; set; }
    }
}
