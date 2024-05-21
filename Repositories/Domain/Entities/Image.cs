using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Core.Domain.Entities
{
	public class Image
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid ImageId { get; set; }

		[NotMapped]
		[Required]
		public IFormFile File { get; set; }

		[Required(ErrorMessage = "The File Name is required.")]
		[StringLength(50, ErrorMessage = "The File Name field must not exceed 100 characters.")]
		public string FileName { get; set; }

		[StringLength(50, ErrorMessage = "The File Name field must not exceed 100 characters.")]
		public string? FileDescription { get; set; }

		[Required(ErrorMessage = "The File Extension is required.")]
		public string FileExtension { get; set; }
		public long FileSizeInBytes { get; set; }
		public string FilePath { get; set; }
	}
}
