using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Project.Core.DTO.Images;

namespace Project.API.Filters.CustomActionFilters
{
	public class ValidateFileUploadAttribute: ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			if(context.ActionArguments.TryGetValue(nameof(ImageUploadRequest), out var arg) && arg is ImageUploadRequest imageUploadRequest)
			{
				var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

				if(!allowedExtensions.Contains(Path.GetExtension(imageUploadRequest.File.FileName)))
				{
					context.ModelState.AddModelError("file", "Unsupported file extension");
				}

				if (imageUploadRequest.File.Length > 10485760)
				{
					context.ModelState.AddModelError("file", "File size more than 10MB, please upload a smaller size file.");
				}

				if(!context.ModelState.IsValid)
				{
					var errorMessages = context.ModelState.Values
						.SelectMany(v => v.Errors)
						.Select(e => e.ErrorMessage)
						.ToList();

					context.Result = new BadRequestObjectResult(errorMessages);
				}
			}

			base.OnActionExecuting(context);
		}
	}
}
