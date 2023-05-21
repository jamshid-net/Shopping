using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shopping.Application.Abstraction;
using Shopping.Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Infrastructure
{
    public static class Startup
    {
        public static IServiceCollection AddInfrastructure(this  IServiceCollection services, IConfiguration configuration) 
        {
            services.AddDbContext<IApplicationDbContext, ProductDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DbConnection"));
                

            });
            return services;    
        }

    }
}
