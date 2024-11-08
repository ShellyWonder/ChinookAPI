namespace ChinookInterviewYT.Client.Models.DTOs
{
    public class SearchResultDTO<T>
    {
        public List<T>? Results { get; set; }
        public int TotalCount { get; set; }
    }
}
