﻿namespace NZWalksAPI.Models.Domein
{
    public class Walk: BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        public Guid DifficultyId { get; set; }
        public Difficulty Difficulty { get; set; }
        public Guid RegionId { get; set; }
        public Region Region { get; set; }
    }
}
