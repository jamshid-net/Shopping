


using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace ProductWebApi.Attributes
{
    public class CustomExceptionAttribute: ExceptionFilterAttribute
    {
        public override async void OnException(ExceptionContext context)
        {
            int exception = context.Exception switch
            {
                NullReferenceException =>
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest,
                Exception =>
                context.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound,



            };
            //Log.Error(exception.)
            
           


        }

       

        public override Task OnExceptionAsync(ExceptionContext context)
        {
            return base.OnExceptionAsync(context);
        }
    }
}
