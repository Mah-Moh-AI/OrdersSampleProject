using Project.Core.ServiceContracts.IEnvironmentService;

namespace Project.API.EnvironmentService
{
	public class EnvironmentService : IEnvironmentService
	{
		private readonly IWebHostEnvironment webHostEnvironment;

		public EnvironmentService(IWebHostEnvironment webHostEnvironment)
        {
			this.webHostEnvironment = webHostEnvironment;
		}
        public string ContentRootPath => webHostEnvironment.ContentRootPath;
	}
}
