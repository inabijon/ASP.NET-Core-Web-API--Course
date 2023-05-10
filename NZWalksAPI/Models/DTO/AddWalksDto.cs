using NZWalksAPI.Models.Domein;
using System.ComponentModel.DataAnnotations;

namespace NZWalksAPI.Models.DTO
{
    public class AddWalksDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Code has to be a min of 3 characters")]
        [MaxLength(100, ErrorMessage = "Code has to be a max of 100 characters")]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string Description { get; set; }
        [Required]
        [MinLength(1)]
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }
    }
}
