using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Project.API.V1.Controllers
{
    [Route("api/v{version:apiVersion}/[Controller]")]
	[ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
	[AllowAnonymous]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "FreezingV1", "BracingV1", "ChillyV1", "CoolV1", "MildV1", "WarmV1", "BalmyV1", "HotV1", "SwelteringV1", "ScorchingV1"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        //[ApiVersionNeutral]
        public IEnumerable<WeatherForecast> GetV1()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

		[HttpGet]
		[MapToApiVersion("2.0")]
		//[ApiVersionNeutral]
		public IEnumerable<WeatherForecast> GetV2()
		{
			return Enumerable.Range(1, 5).Select(index => new WeatherForecast
			{
				Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
				TemperatureC = Random.Shared.Next(-20, 55),
				Summary = Summaries[Random.Shared.Next(Summaries.Length)]
			})
			.ToArray();
		}
	}
}
