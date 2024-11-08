namespace ChinookInterviewYT.Client.Models.DTOs
{
    public class ArtistDTO
    {
        public int ArtistId { get; set; }
        public string? ArtistName { get; set; }
        public List<Album> Albums { get; set; } = new List<Album>();
    }
}
