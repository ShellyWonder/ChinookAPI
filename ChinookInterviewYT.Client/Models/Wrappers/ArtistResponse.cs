using ChinookInterviewYT.Client.Models.DTOs;

namespace ChinookInterviewYT.Client.Models.Wrappers
{
    public class ArtistsResponse
    {
        public object? Result { get; set; } // Expecting this to remain null or unused
        public PagedArtistDTO? Value { get; set; }  // Contains the paginated artist data
    }

}
