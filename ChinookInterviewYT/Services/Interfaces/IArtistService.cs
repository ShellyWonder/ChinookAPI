using ChinookInterviewYT.Client.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ChinookInterviewYT.Services.Interfaces
{
    public interface IArtistService
    {
        public Task<ActionResult<PagedArtistDTO>> GetAlbumsAndTracksByArtistAsync(int pageNumber, int pageSize);
    }
}
