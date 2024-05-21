using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.DTO.Images
{
	public class ImageUploadRequest
	{
		[Required]
		public IFormFile File { get; set; }

		[Required(ErrorMessage = "The File Name is required.")]
		[StringLength(50, ErrorMessage = "The File Name field must not exceed 100 characters.")]
		public string FileName { get; set; }

		[StringLength(50, ErrorMessage = "The File Name field must not exceed 100 characters.")]
		public string? FileDescription { get; set; }
	}
}
