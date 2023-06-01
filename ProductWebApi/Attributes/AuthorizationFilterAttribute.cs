using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Serilog;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;

namespace ProductWebApi.Attributes
{
    public class AuthorizationFilterAttribute : Attribute,IAuthorizeData, IAsyncAuthorizationFilter, IAuthorizationFilter
    {

        public string? Email { get; set; }
        public string? Policy { get;set;}
        public string? Roles { get;set;}
        public string? Permissions { get; set; }
        public string? AuthenticationSchemes { get; set; } = CookieAuthenticationDefaults.AuthenticationScheme;
       

        public async void OnAuthorization(AuthorizationFilterContext context)
        {
            await PermissionCheck(context);
            var email = context.HttpContext.User.FindFirstValue(ClaimTypes.Email);
            if (email == null)
               throw new Exception();
            
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
           await  PermissionCheck(context);
           var email =  context.HttpContext.User.FindFirstValue(ClaimTypes.Email);
            if(email == null) {
                throw new Exception();
             }
          
        }

        public  Task PermissionCheck(AuthorizationFilterContext context)
        {
            var userPermissions = 
                context.HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.Role);
            foreach (var permission in userPermissions)
            {
                if (Permissions.Contains(permission.Value))
                    return Task.CompletedTask;
               
            }
            throw new UnauthorizedAccessException("user not authorized");  

        }

    }
}
