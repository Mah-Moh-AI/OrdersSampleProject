using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Project.API.Configuration
{
	public class ConfigureSwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
	{
		private readonly IApiVersionDescriptionProvider apiVersionDescriptionProvider;

		public ConfigureSwaggerOptions(IApiVersionDescriptionProvider apiVersionDescriptionProvider)
		{
			this.apiVersionDescriptionProvider = apiVersionDescriptionProvider;
		}

		public void Configure(string? name, SwaggerGenOptions options)
		{
			Configure(options);
		}

		public void Configure(SwaggerGenOptions options)
		{
			foreach(var item in apiVersionDescriptionProvider.ApiVersionDescriptions)
			{
				options.SwaggerDoc(item.GroupName, CreateVersionInfo(item));

			}

			options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
			{
				Name = "Authorization",
				In = ParameterLocation.Header,
				Type = SecuritySchemeType.ApiKey,
				Scheme = JwtBearerDefaults.AuthenticationScheme
			});

			options.AddSecurityRequirement(new OpenApiSecurityRequirement
			{
				{
					new OpenApiSecurityScheme
					{
						Reference = new OpenApiReference
						{
							Type = ReferenceType.SecurityScheme,
							Id = JwtBearerDefaults.AuthenticationScheme
						},
						Scheme = "Bearer",
						Name = JwtBearerDefaults.AuthenticationScheme,
						In = ParameterLocation.Header
					},
					new List<string>()
				}
			});

		}


		private OpenApiInfo CreateVersionInfo(ApiVersionDescription description)
		{
			OpenApiInfo info = new OpenApiInfo
			{
				Title = "Your Versioned API",
				Version = description.ApiVersion.ToString(),
			};

			return info;
		}

	}
}
