using ChinookInterviewYT.Data.Repositories.Interfaces;
using ChinookInterviewYT.Client.Models.DTOs;
using ChinookInterviewYT.Client.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace ChinookInterviewYT.Data.Repositories
{
    public class ArtistRepository : IArtistRepository
    {
    private readonly ChinookDbContext _context;

        public ArtistRepository(ChinookDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<PagedArtistDTO>> GetAlbumsAndTracksByArtistAsync(int pageNumber, int pageSize)
        {
            var artists = await _context.Artists
                                 .Select(a => new ArtistDTO
                                 {
                                     ArtistId = a.ArtistId,
                                     ArtistName = a.Name,
                                     Albums = a.Albums.Select(album => new AlbumDTO
                                     {
                                         AlbumId = album.AlbumId,
                                         AlbumTitle = album.Title,
                                         //AlbumUrl = album.AlbumUrl,
                                         TotalAlbumDuration = album.Tracks.Sum(track => track.Milliseconds),
                                         Tracks = album.Tracks.Select(track => new TrackDTO
                                         {
                                             TrackId = track.TrackId,
                                             TrackTitle = track.Name,
                                             Composer = track.Composer,
                                             Milliseconds = track.Milliseconds,
                                             UnitPrice = track.UnitPrice,
                                         })
                                         .OrderBy(track => track.TrackTitle)
                                         .ToList()
                                     })
                                     .OrderBy(album => album.AlbumTitle)
                                     .ToList()
                                 })
                                 .OrderBy(a => a.ArtistName)
                                 .Skip((pageNumber - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToListAsync();

            int TotalCount = await _context.Artists.CountAsync();
            var PagedArtists = new PagedArtistDTO
            {
                Artists = artists,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = TotalCount
            };
            return PagedArtists;
        }
    }
}
