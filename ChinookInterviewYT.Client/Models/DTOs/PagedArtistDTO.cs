using System.Text.Json.Serialization;

namespace ChinookInterviewYT.Client.Models.DTOs
{
    public class PagedArtistDTO
    {
        [JsonPropertyName("result")] // Maps the JSON "results" to the "Artists" property
        public List<ArtistDTO> Artists { get; set; } = [];
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        [JsonPropertyName("totalCount")] // Maps JSON "totalCount" to this property
        public int TotalCount { get; set; } = 0;
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    }
}
