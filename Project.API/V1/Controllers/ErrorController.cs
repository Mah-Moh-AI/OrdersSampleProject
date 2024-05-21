using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Project.API.V1.Controllers
{
    ///
    /// Not implmented yet. Just for reference.
    ///
    /// 
    /// 
    /// 
    /// <summary>
    /// Controller responsible for handeling errors and return error responses
    /// </summary>
    [Route("error")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        /// <summary>
        /// Handles error requests and return custom error response
        /// </summary>
        /// <returns>A custom error response.</returns>
        [HttpGet("")]
        public IActionResult Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            var exception = context.Error;

            return Problem(
                detail: exception.Message,
                title: "An error occured",
                statusCode: 500
                );
        }
    }
}
