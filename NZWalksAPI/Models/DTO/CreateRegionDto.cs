using System.ComponentModel.DataAnnotations;

namespace NZWalksAPI.Models.DTO
{
    public class CreateRegionDto
    {
        [Required]
        [MaxLength(3, ErrorMessage = "Code has to be a max of 3 characters")]
        [MinLength(2, ErrorMessage = "Code has to be a min of 2 characters")]
        public string Code { get; set; }
        [Required]
        [MaxLength(80, ErrorMessage = "Code has to be a min of 80 characters")]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
