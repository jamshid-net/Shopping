using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Serilog;
using System.Net;
using System.Threading.Tasks;

namespace ProductWebApi.ExceptionHandler
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
                if(httpContext.Response.StatusCode < 300)
                {
                Log.Warning("CLIENT_IP:{ClientIp} CLIENT:{EnvironmentUserName}" + $" Request:{httpContext.Request.Path} Status_Code:{httpContext.Response.StatusCode}");

                }
                else
                {
                    Log.Error("⚠️CLIENT_IP:{ClientIp} CLIENT:{EnvironmentUserName}" + $" Request:{httpContext.Request.Path} Status_Code:{httpContext.Response.StatusCode}");
                }
            }
            catch (KeyNotFoundException ex)
            {
              await  HandleExceptionAsync
                    (httpContext, ex.Message, HttpStatusCode.NotFound, "Not found for your request!");
            }
            catch (Exception ex)
            {

             await   HandleExceptionAsync
                   (httpContext, ex.Message, HttpStatusCode.InternalServerError, "Oops something went wrong!");

            }

           
        }

        public async Task HandleExceptionAsync
            (HttpContext httpContext,string exMessage, HttpStatusCode httpStatusCode, string message)
        {
            Log.Error("EXCEPTION:🔴 CLIENT_IP:{ClientIp}  CLIENT:{EnvironmentUserName} AGENT:{ClientAgent}" + $"\nDatetime:{DateTime.Now} | Message:{exMessage} | Path:{httpContext.Request.Path}");
            HttpResponse response = httpContext.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)httpStatusCode;
           

            ErrorDto error = new ErrorDto
            {
                Message = message,
                StatusCode = (int)httpStatusCode
            };

           await response.WriteAsync(error.ToString());

        }
    }

    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
