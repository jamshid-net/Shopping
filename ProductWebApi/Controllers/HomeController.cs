using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProductWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

      


        [Authorize]
        [HttpGet("/productPage.html")]
       
        public async Task<ContentResult> ProductPage()
        {

            string webrootpath = _webHostEnvironment.WebRootPath;
            string path =  Path.Combine(webrootpath, "productPage.html");
            var html = System.IO.File.ReadAllText(path);
            
            return new ContentResult { Content = html , ContentType = "text/html",StatusCode=200};

        }
        
        
        [Authorize]
        [HttpGet("/swagger/index.html")]
        public async Task<IActionResult> SwaggerPage()
        {
            return await Task.FromResult(Redirect("/swagger/index.html"));
        }

        [HttpGet("/logout")]
        public async Task<IActionResult> Logout()
        {
            var context = HttpContext;
            await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }
        

    }
}
