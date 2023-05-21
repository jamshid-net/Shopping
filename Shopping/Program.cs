using Shopping.Application;
using Shopping.Infrastructure;

namespace Shopping
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            IConfiguration configuration = builder.Configuration;
            builder.Services.AddApplication(configuration);
            builder.Services.AddInfrastructure(configuration);
            builder.Services.AddControllers();
            var app = builder.Build();
            app.MapControllers();
            
            

            app.Run();
        }
    }
}