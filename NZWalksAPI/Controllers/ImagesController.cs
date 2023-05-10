using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Models.Domein;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Repositories.IRepository;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        public readonly IIImageRepository IImageRepository ;
        public ImagesController(IIImageRepository iImageRepository)
        {
            IImageRepository = iImageRepository;
        }


        [HttpPost]
        [Route("Upload")]
        [Authorize(Roles = "Creater")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto image )
        { 
            ValidateFileUpload(image);

            if(ModelState.IsValid)
            {
                // convert DTO to Domain model
                var imageDomainModel = new Image
                {
                    File = image.File,
                    FileDescription = image.FileDescription,
                    FileName = image.FileName,
                    FileExtention = Path.GetExtension(image.File.FileName).Trim(),
                    FileSizeInBytes = image.File.Length,
                };

                // User repository to upload image
                await IImageRepository.Upload(imageDomainModel);

                return Ok(imageDomainModel);
            }

            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(ImageUploadRequestDto imageUploadRequest)
        {
            var allowedExtentions = new String[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtentions.Contains(Path.GetExtension(imageUploadRequest.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extention");
            }

            if(imageUploadRequest.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size more than 10mb, please upload smaller size");
            }

        }
    }
}
