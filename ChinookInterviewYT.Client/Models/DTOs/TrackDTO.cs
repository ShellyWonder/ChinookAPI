namespace ChinookInterviewYT.Client.Models.DTOs
{
    public class TrackDTO
    {
        public int TrackId { get; set; }
        public string TrackTitle { get; set; } = null!;
        public string? Composer { get; set; }
        public int Milliseconds { get; set; }
        public decimal? UnitPrice { get; set; }
    }
}
