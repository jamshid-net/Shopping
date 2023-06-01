using Microsoft.Extensions.DependencyInjection;
using Shopping.Application.Service;

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
            services.AddScoped<ICartItemService,CartItemService>();
            services.AddScoped<IHashStringService, HashStringService>();    
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IUserTokenService, UserTokenService>();
            services.AddScoped<IOrderRepository, OrderService>();
            //services.AddScoped<IValidator<UserLogin>, UserLoginValidate>();
            //services.AddScoped<IValidator<UserRegister>, UserRegisterValidate>();
            //services.AddScoped<IValidator<UserCreate>, UserCreateValidate>();
            //services.AddScoped<IValidator<UserUpdate>, UserUpdateValidate>();
            //services.AddScoped<IValidator<ProductCreate>, ProductCreateValidate>();
            //services.AddScoped<IValidator<ProductUpdate>, ProductUpdateValidate>();
            //services.AddScoped<IValidator<RoleCreate>, RoleCreateValidate>();
            //services.AddScoped<IValidator<RoleUpdate>, RoleUpdateValidate>();
            //services.AddScoped<IValidator<PermissionCreate>, PermissionCreateValidate>();
            //services.AddScoped<IValidator<PermissionUpdate>, PermissionUpdateValidate>();
            //services.AddScoped<IValidator<OrderCreate>, OrderCreateValidate>();
            //services.AddScoped<IValidator<OrderUpdate>, OrderUpdateValidate>();
            //services.AddScoped<IValidator<CategoryCreate>, CategoryCreateValidate>();
            //services.AddScoped<IValidator<CategoryUpdate>, CategoryUpdateValidate>();

            return services;

          
        }
    }
 
}
