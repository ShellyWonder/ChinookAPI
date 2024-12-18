﻿namespace ChinookInterviewYT.Client.Models.DTOs
{
    public class AlbumDTO
    {
        public int AlbumId { get; set; }
        public string? AlbumTitle { get; set; } = null!;
        public List<TrackDTO> Tracks { get; set; } = new List<TrackDTO>();
        public int TotalAlbumDuration { get; set; } = 0;
    }
}
