using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Project.API.Filters.CustomActionFilters
{
	public class ValidateModelAttribute : ActionFilterAttribute
	{
        public override void OnActionExecuting(ActionExecutingContext context)
		{

			if (context.ModelState.IsValid != true)
			{
				var errorMessages = context.ModelState.Values
					.SelectMany(v => v.Errors)
					.Select(e => e.ErrorMessage)
					.ToList();

				// logger.LogWarning("Model is not Valid. Errors: {errors}", string.Join(", ", errorMessages));
				context.Result = new BadRequestObjectResult(errorMessages);
			}
		}
	}
}
