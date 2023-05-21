using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ProductWebApi
{
    public static class Startup
    {

        #region JWTTOKEN
        //public static AuthenticationBuilder AddCustomJwtBearer(this AuthenticationBuilder builder, IConfiguration configuration)
        //{
        //    builder.AddJwtBearer(x =>
        //     {
        //         x.SaveToken = true;
        //         x.TokenValidationParameters = new TokenValidationParameters()
        //         {
        //             ValidateAudience = true,
        //             ValidateIssuer = true,
        //             ValidAudience = configuration["JWT:Audience"],
        //             ValidIssuer = configuration["JWT:Issuer"],
        //             ValidateLifetime = true,
        //             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"])),
        //             ClockSkew = TimeSpan.Zero

        //         };

        //         x.Events = new JwtBearerEvents()
        //         {
        //             OnAuthenticationFailed = context =>
        //             {
        //                 if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
        //                 {
        //                     context.Response.Headers.Add("IS-TOKEN-EXPIRED", "true");
        //                 }
        //                 return Task.CompletedTask;
        //             }
        //         };
        //     });
        //    return builder;

        //}
        #endregion 


        #region CookieAuthen
        public static AuthenticationBuilder AddCustomCookie(this AuthenticationBuilder builder)
        {
            

            return builder;
        }
        #endregion

    }
}
