using AutoMapper;
using Project.Core.Domain.Entities;
using Project.Core.DTO.Images;


namespace Project.API.Mapping
{
	public class AutoMapperProfiles: Profile
	{
		public AutoMapperProfiles() 
		{ 
			CreateMap<Image, ImageResponse>().ReverseMap();

			
			CreateMap<ImageUploadRequest, Image>()
				.ForMember(dest => dest.FileExtension, opt => opt.MapFrom(src => Path.GetExtension(src.File.FileName)))
				.ForMember(dest => dest.FileSizeInBytes, opt => opt.MapFrom(src => src.File.Length));			
		}
	}
}
