using ChinookInterviewYT.Client.Models.DTOs;
using ChinookInterviewYT.Services;
using ChinookInterviewYT.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChinookInterviewYT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController(IArtistService artistService) : ControllerBase
    {
        private readonly IArtistService _artistService = artistService;

        [HttpGet("AlbumsandTracksPaged")]
        public async Task<ActionResult<PagedArtistDTO>> GetAlbumsAndTracksPagedAsync([FromQuery] int pageNumber = 1, int pageSize = 5)
        {
            try
            {
                //Validate parameters
                if (pageNumber < 1) pageNumber = 1;
                if (pageSize < 1 || pageSize > 20) pageSize = 5;

                //Get data 
                var PagedArtists = await _artistService.GetAlbumsAndTracksByArtistAsync(pageNumber, pageSize);
                return Ok(PagedArtists);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return Problem();
            }
            
        }
    }
}
