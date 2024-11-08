using ChinookInterviewYT.Client.Models.DTOs;
using ChinookInterviewYT.Data.Repositories.Interfaces;
using ChinookInterviewYT.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChinookInterviewYT.Services
{
    public class ArtistService : IArtistService
    {
        private readonly IArtistRepository _artistRepository;

        public ArtistService(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
        }

        public async Task<ActionResult<PagedArtistDTO>> GetAlbumsAndTracksByArtistAsync(int pageNumber, int pageSize)
        {
            return await _artistRepository.GetAlbumsAndTracksByArtistAsync(pageNumber, pageSize);
        }
    }
}
