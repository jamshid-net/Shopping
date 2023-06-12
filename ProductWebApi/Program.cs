using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ProductWebApi.AutoMapping;
using ProductWebApi.ExceptionHandler;
using Serilog;
using Serilog.Events;
using Shopping.Application;
using Shopping.Application.Service;
using Shopping.Infrastructure;
using TelegramSink;

namespace ProductWebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .MinimumLevel.Warning()
            .WriteTo.Console()
            .Enrich.FromLogContext()
            .Enrich.WithEnvironmentUserName()
            .Enrich.WithMachineName()
            .Enrich.WithClientIp()
            .WriteTo.TeleSink(
             telegramApiKey: builder.Configuration.GetConnectionString("TelegramToken"),
             telegramChatId: "33780774",
             minimumLevel: LogEventLevel.Error

             )
            .CreateLogger();

            builder.Host.UseSerilog();
            builder.Services.AddResponseCaching();
            builder.Services.AddLazyCache();

            builder.Services.AddLimiters();
            builder.Services.AddControllers();
            IConfiguration configuration = builder.Configuration;
            builder.Services.AddAutoMapper(typeof(AppMapping));

            builder.Services.AddApplication(configuration);
            builder.Services.AddInfrastructure(configuration);


            //builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddCustomJwtBearer(configuration);
            //builder.Services.AddTelegramBot(configuration, builder.Environment.WebRootPath + "\\photos\\");


            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)

            .AddCookie(options =>
            {

                options.LoginPath = "/";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(15);
                options.Cookie.MaxAge = options.ExpireTimeSpan;
                options.SlidingExpiration = true;


            });
            builder.Services.AddAuthorization();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(
             //   options =>
             //   {
             //    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
             //    {
             //        Scheme = "Bearer",
             //        BearerFormat = "JWT",
             //        In = ParameterLocation.Header,
             //        Name = "Authorization",
             //        Description = "Bearer Authentication with JWT Token",
             //        Type = SecuritySchemeType.Http
             //    });
             //    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
             //       {{
             //   new OpenApiSecurityScheme()
             //   {
             //      Reference=new OpenApiReference()
             //      {
             //          Id="Bearer",
             //          Type=ReferenceType.SecurityScheme
             //      }
             //   },
             //   new List<string>()
             //    } });
             //}                               
             );

            var app = builder.Build();
           
            app.UseRateLimiter();
            app.UseWhen(context => context.Request.Path == "/time",
            appbuilder =>
            {
                appbuilder.Use(async (context, next) =>
                    {
                        var time = DateTime.Now.ToShortTimeString();
                        Console.WriteLine($"Time: {time}");
                        await next();  
                    });

                
                appbuilder.Run(async context =>
                    {
                        var time = DateTime.Now.ToShortTimeString();
                        await context.Response.WriteAsync($"Time: {time}");
                    });
            });
            app.UseDirectoryBrowser("/pages");
            app.UseFileServer();
            app.UseStaticFiles();
            app.UseDefaultFiles();

            app.UseHttpsRedirection();
            app.UseExceptionMiddleware();
          // app.UseResponseCaching();
            app.UseAuthentication();
            app.UseAuthorization();
            
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.DisplayRequestDuration();
                });
            }
            app.MapControllers();


            await app.RunAsync();

        }
    }
}