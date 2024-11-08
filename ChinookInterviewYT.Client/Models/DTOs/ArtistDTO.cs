namespace ChinookInterviewYT.Client.Models.DTOs
{
    public class ArtistDTO
    {
        public int ArtistId { get; set; }
        public string? ArtistName { get; set; }
        public List<AlbumDTO> Albums { get; set; } = new List<AlbumDTO>();
    }
}
