﻿using Microsoft.Identity.Client;
using System.Net;

namespace Project.API.Middlewares
{
	public class ExceptionHandlerMiddleware
	{
		private readonly ILogger<ExceptionHandlerMiddleware> logger;
		private readonly RequestDelegate next;

		public ExceptionHandlerMiddleware(
            ILogger<ExceptionHandlerMiddleware> logger,
            RequestDelegate next)
        {
			this.logger = logger;
			this.next = next;
		}

		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				await next(httpContext);
			}
			catch(Exception ex)
			{
				Guid errorId = Guid.NewGuid();

				logger.LogError(ex, "{errorId} : {message}", errorId, ex.Message);

				httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

				httpContext.Response.ContentType = "application/json";

				var error = new
				{
					Id = errorId,
					ErrorMessage = "Something went wrong! Please try again later"
				};

				await httpContext.Response.WriteAsJsonAsync(error);

			}
		}
    }
}
