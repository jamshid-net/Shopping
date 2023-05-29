using Microsoft.EntityFrameworkCore;
using Shopping.Application.Abstraction;
using Shopping.Application.Interfaces;
using Shopping.Domain.Models;
using System.Linq.Expressions;

namespace Shopping.Application.Service
{
    public class CartItemService : ICartItemService
    {
        private readonly IApplicationDbContext _dbContext;

        public CartItemService(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IQueryable<CartItem>> GetAllAsync()
        {
            return   _dbContext.CartItems.Include(x=>x.User).Include(x=>x.Product);
        }

        public async Task<CartItem> GetByIdAsync(int id)
        {
            return await _dbContext.CartItems.FindAsync(id);
        }

        public async Task<bool> CreateAsync(CartItem entity)
        {
            await _dbContext.CartItems.AddAsync(entity);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(CartItem entity)
        {
            _dbContext.CartItems.Update(entity);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var cartItem = await GetByIdAsync(id);
            if (cartItem != null)
            {
                _dbContext.CartItems.Remove(cartItem);
                return await _dbContext.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> DeleteAsyncExpression(Expression<Func<CartItem, bool>> expression)
        {
            var cartItems = _dbContext.CartItems.Where(expression);
             _dbContext.CartItems.RemoveRange(cartItems);
            return await _dbContext.SaveChangesAsync() >0;
             
        }
    }
}
