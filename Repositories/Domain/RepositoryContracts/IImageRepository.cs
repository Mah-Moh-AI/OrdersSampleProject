using Project.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.Domain.RepositoryContracts
{
	public interface IImageRepository
	{
		Task<Image> Upload(Image image);
	}
}
