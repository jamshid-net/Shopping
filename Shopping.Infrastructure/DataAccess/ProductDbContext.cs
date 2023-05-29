using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Shopping.Application.Abstraction;
using Shopping.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Infrastructure.DataAccess
{
    public class ProductDbContext : DbContext, IApplicationDbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> dbContextOptions)
              : base(dbContextOptions) { }


       
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; } 
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
       
        public DbSet<User> Users { get; set; }

        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        
        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
       


    }
}
