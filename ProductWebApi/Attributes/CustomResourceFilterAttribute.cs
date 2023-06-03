using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using ProductWebApi.ExceptionHandler;
using Serilog;
using System.Net;

namespace ProductWebApi.Attributes
{
    public class CustomResourceFilterAttribute : Attribute, IResourceFilter
    {
        
        public CustomResourceFilterAttribute()
        {
            
        }
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            throw new NotImplementedException();
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            throw new NotImplementedException();
        }
    }
}
