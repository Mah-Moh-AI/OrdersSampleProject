using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.API.Filters.CustomActionFilters;
using Project.Core.DTO.Images;
using Project.Core.ServiceContracts.Images;
using Project.Core.Services.Images;

namespace Project.API.V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly ILogger<ImagesController> logger;
        private readonly IImageService imageService;

        public ImagesController(
            ILogger<ImagesController> logger,
            IImageService imageService)
        {
            this.logger = logger;
            this.imageService = imageService;
        }

        [HttpPost]
        [Route("Upload")]
        [Authorize]
        [ValidateModel]
        [ValidateFileUpload]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequest imageUploadRequest)
        {
            logger.LogInformation("Uploaded file is validated");
            logger.LogInformation("Uploading Image...");
            logger.LogDebug("image name {name}", imageUploadRequest.File.FileName);

            ImageResponse imageResponse = await imageService.Upload(imageUploadRequest);

            logger.LogInformation("Image is uploaded successfully");

            return Ok(imageResponse);
        }

    }
}
