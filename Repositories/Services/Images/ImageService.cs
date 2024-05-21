using AutoMapper;
using Microsoft.Extensions.Logging;
using Project.Core.Domain.Entities;
using Project.Core.Domain.RepositoryContracts;
using Project.Core.DTO.Images;
using Project.Core.ServiceContracts.Images;
using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.Services.Images
{
	public class ImageService : IImageService
	{
		
		private readonly ILogger<ImageService> logger;
		private readonly IMapper mapper;
		private readonly IImageRepository imageRepository;

		public ImageService(
			ILogger<ImageService> logger, 
			IMapper mapper, 
			IImageRepository imageRepository)
        {
			this.logger = logger;
			this.mapper = mapper;
			this.imageRepository = imageRepository;
		}
        public async Task<ImageResponse> Upload(ImageUploadRequest imageUploadRequest)
		{
			logger.LogInformation("Uploading Image...");

			Image imageDomain = mapper.Map<Image>(imageUploadRequest);

			Image uploadedImage = await imageRepository.Upload(imageDomain);

			ImageResponse imageResponse = mapper.Map<ImageResponse>(uploadedImage);

			logger.LogInformation("Image is Uploaded");

			return imageResponse;

		}

	}
}
