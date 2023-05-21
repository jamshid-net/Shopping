using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shopping.Application.Abstraction;
using Shopping.Application.Interfaces;
using Shopping.Application.Service;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace Shopping.Application
{
    public static class Startup
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IRolePermissionService,RolePermissionService>();
            services.AddScoped<IHashStringService, HashStringService>();    
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IUserTokenService, UserTokenService>();
            services.AddScoped<IOrderRepository, OrderService>();
            
          
            return services;
        }
    }
}
