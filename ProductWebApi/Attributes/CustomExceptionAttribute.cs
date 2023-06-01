using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProductWebApi.ExceptionHandler;
using Serilog;
using System.Security;
using System.Security.Authentication;

namespace ProductWebApi.Attributes
{
    public class CustomExceptionAttribute : ExceptionFilterAttribute
    {

        public override async Task OnExceptionAsync(ExceptionContext context)
        {
           
            var ex = context.Exception;
            if (ex is ArgumentException)
                context.Result = await HandleExceptionAsync(ex, StatusCodes.Status400BadRequest);
            else if (ex is AuthenticationException)
                context.Result = await HandleExceptionAsync(ex, StatusCodes.Status401Unauthorized);
            else if (ex is SecurityException)
                context.Result = await HandleExceptionAsync(ex, StatusCodes.Status403Forbidden);
            else if (ex is KeyNotFoundException || ex is NullReferenceException)
                context.Result = await HandleExceptionAsync(ex, StatusCodes.Status404NotFound);
            else 
            context.Result = await HandleExceptionAsync(ex, StatusCodes.Status500InternalServerError);


        }

        public  async Task<IActionResult> HandleExceptionAsync(Exception ex, int statusCode)
        {
            Log.Error(ex.Message, statusCode);
            var error = new ErrorDto
            {
                Message = ex.Message,
                StatusCode = statusCode
            };
            return await Task.FromResult(new OkObjectResult(error));
        }

    }
}
