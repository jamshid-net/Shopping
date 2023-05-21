using Microsoft.EntityFrameworkCore;
using Shopping.Application.Abstraction;
using Shopping.Application.Interfaces;
using Shopping.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Application.Service
{
    public class OrderService:IOrderRepository
    {
        private readonly IApplicationDbContext _dbContext;

        public OrderService(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
           
        }

        public async Task<bool> CreateAsync(Order entity)
        {
            await _dbContext.Orders.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var order = await _dbContext.Orders.FindAsync(id);
            if (order == null)
                return false;

            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IQueryable<Order>> GetAllAsync()
        {
            var orders = _dbContext.Orders.Include(x=>x.User)
                .Include(x=>x.OrderProducts)
                .ThenInclude(x=>x.Product);
            return await Task.FromResult(orders);
        }

        public async Task<Order> GetAsync(Expression<Func<Order, bool>> expression)
        {
            var order = _dbContext.Orders.Where(expression)?
                .Include(x => x.User)?
                .Include(x => x.OrderProducts)?
                .ThenInclude(x => x.Product).FirstOrDefault();
            return order;
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            var order = await _dbContext.Orders.Include(x => x.User)
                .Include(x => x.OrderProducts)
                .ThenInclude(x => x.Product).
                FirstOrDefaultAsync(x=>x.OrderId == id);
            return order;
        }

        public async Task<bool> UpdateAsync(Order entity)
        {
            _dbContext.Orders.Update(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
