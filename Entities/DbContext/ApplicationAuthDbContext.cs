using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure
{
	public class ApplicationAuthDbContext: IdentityDbContext
	{
        public ApplicationAuthDbContext(DbContextOptions<ApplicationAuthDbContext> options) : base(options)
        {
            
        }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			var clientRoleId = "a71a55d6-99d7-4123-b4e0-1218ecb90e3e";
			var adminRoleId = "c309fa92-2123-47be-b397-a1c77adb502c";
			var systemAdminRoleId = "D0C17EFF-E049-49B8-8A02-BA4CBFD006F0";

			List<IdentityRole> roles = new List<IdentityRole>()
			{
				new IdentityRole
				{
					Id = clientRoleId,
					ConcurrencyStamp = clientRoleId,
					Name = "Client",
					NormalizedName = "Client".ToUpper()
				},

				new IdentityRole
				{
					Id= adminRoleId,
					ConcurrencyStamp= adminRoleId,
					Name = "Admin",
					NormalizedName = "Admin".ToUpper()
				},

				new IdentityRole
				{
					Id= systemAdminRoleId,
					ConcurrencyStamp= systemAdminRoleId,
					Name = "SystemAdmin",
					NormalizedName = "SystemAdmin".ToUpper()
				}
			};

			builder.Entity<IdentityRole>().HasData(roles);
		}
	}
}
