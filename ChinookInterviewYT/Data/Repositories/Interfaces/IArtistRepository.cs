using ChinookInterviewYT.Client.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ChinookInterviewYT.Data.Repositories.Interfaces
{
    public interface IArtistRepository
    {
        public Task<ActionResult<PagedArtistDTO>> GetAlbumsAndTracksByArtistAsync(int pageNumber, int pageSize);
    }
}
