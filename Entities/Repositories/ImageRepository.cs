using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Project.Core.Domain.Entities;
using Project.Core.Domain.RepositoryContracts;
using Project.Core.ServiceContracts.IEnvironmentService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.Repositories
{
	public class ImageRepository : IImageRepository
	{
	
		private readonly ILogger<ImageRepository> logger;
		private readonly IEnvironmentService environmentService;
		private readonly IHttpContextAccessor httpContextAccessor;
		private readonly ApplicationDbContext applicationDbContext;

		public ImageRepository(
			ILogger<ImageRepository> logger, 
			IEnvironmentService environmentService,
			IHttpContextAccessor httpContextAccessor,
			ApplicationDbContext applicationDbContext)
        {
			this.logger = logger;
			this.environmentService = environmentService;
			this.httpContextAccessor = httpContextAccessor;
			this.applicationDbContext = applicationDbContext;
		}
		public async Task<Image> Upload(Image image)
		{
			logger.LogInformation("Uploading Image {image}...", image.FileName.ToString());

			string localFilePath = Path.Combine(environmentService.ContentRootPath, "Images", $"{image.FileName}{image.FileExtension}");
			logger.LogDebug("Local file path: {path}", localFilePath);

			// Upload image to local Path
			using FileStream stream = new FileStream(localFilePath, FileMode.Create);
			await image.File.CopyToAsync(stream);

			// The below link should be created in Application/UI layer --> against CA fundumentals
			string urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/api/Images/{image.FileName}{image.FileExtension}";
			logger.LogDebug("url File Path {url}", urlFilePath);
			image.FilePath = urlFilePath;

			logger.LogInformation("Adding image data to database....");
			await applicationDbContext.Images.AddAsync(image);
			await applicationDbContext.SaveChangesAsync();

			logger.LogInformation("Image {image} is uploaded successfully", image.FileName.ToString());

			return image;
		}
		
	}

}
