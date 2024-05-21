using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Project.Core.Domain.RepositoryContracts
{
	public interface ITokenRepository
	{
		public string CreateJwtToken(IdentityUser user, List<string> roles);
	}
}
