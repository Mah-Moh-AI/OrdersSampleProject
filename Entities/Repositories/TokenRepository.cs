using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Project.Core.Domain.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.Repositories
{
	public class TokenRepository : ITokenRepository
	{
		private readonly ILogger<TokenRepository> logger;
		private readonly IConfiguration configuration;

		public TokenRepository(ILogger<TokenRepository> logger, IConfiguration configuration)
        {
			this.logger = logger;
			this.configuration = configuration;
		}
        public string CreateJwtToken(IdentityUser user, List<string> roles)
		{
			logger.LogInformation("Creating Token ...");

			List<Claim> claims = new List<Claim>();

			claims.Add(new Claim(ClaimTypes.Email, user.Email));

			foreach(var role in roles)
			{
				claims.Add(new Claim(ClaimTypes.Role, role));
			}

			SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

			SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			JwtSecurityToken token = new JwtSecurityToken(
				configuration["Jwt:Issuer"],
				configuration["Jwt:Audience"],
				claims,
				expires: DateTime.Now.AddMinutes(10),
				signingCredentials: credentials
				);

			logger.LogInformation("Token is created successfully");
			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
