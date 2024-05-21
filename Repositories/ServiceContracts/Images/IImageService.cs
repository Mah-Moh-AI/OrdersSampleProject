using Project.Core.DTO.Images;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.ServiceContracts.Images
{
	public interface IImageService
	{
		Task<ImageResponse> Upload(ImageUploadRequest imageUploadRequest);
	}
}
