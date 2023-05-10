using NZWalksAPI.Data;
using NZWalksAPI.Models.Domein;
using NZWalksAPI.Repositories.IRepository;

namespace NZWalksAPI.Repositories.Repositories
{
    public class ImageRepository : IIImageRepository
    {
        public readonly IWebHostEnvironment WebHost;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly NZContext nZContext;

        public ImageRepository(IWebHostEnvironment webHost, 
            IHttpContextAccessor httpContextAccessor,
            NZContext nZContext)
        {
            WebHost = webHost;
            this.httpContextAccessor = httpContextAccessor;
            this.nZContext = nZContext;
        }


        public async Task<Image> Upload(Image image)
        {
            var localFilePath = Path.Combine(
                WebHost.ContentRootPath, "Images",
                $"{image.FileName}{image.FileExtention}");

            // Upload image to localpath
            using var steam = new FileStream(localFilePath, FileMode.Create);

            await image.File.CopyToAsync(steam);

            // https://localhost:1234/images/image.jpg
            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtention}";

            image.FilePath = urlFilePath;

            //Add Image to Img Table
            await nZContext.Images.AddAsync(image);
            await nZContext.SaveChangesAsync();

            return image;
        }
    }
}
