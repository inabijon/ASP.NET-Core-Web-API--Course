using System.ComponentModel.DataAnnotations.Schema;

namespace NZWalksAPI.Models.Domein
{
    public class Image: BaseModel
    {
        [NotMapped]
        public IFormFile File { get; set; }
        public string FileName { get; set; }
        public string? FileDescription { get; set; }
        public string FileExtention { get; set; }
        public long FileSizeInBytes { get; set; }
        public string FilePath { get; set; }
    }
}
