using Microsoft.EntityFrameworkCore;
using Shopping.Application.Abstraction;
using Shopping.Application.Interfaces;
using Shopping.Domain.Models;

namespace Shopping.Application.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly IApplicationDbContext _dbContext;

        public CategoryService(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--CATEGORY SERVICE WORKING Transient---");
            Console.ResetColor();
        }

        public async Task<bool> CreateAsync(Category entity)
        {
            try
            {
                _dbContext.Categories.Add(entity);
                var res = await _dbContext.SaveChangesAsync();
                if (res != 0)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                Category? category = _dbContext.Categories.FirstOrDefault(x => x.CategoryId == id);
                _dbContext.Categories.Remove(category);
                var res = await _dbContext.SaveChangesAsync();
                if (res != 0)
                    return true;
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IQueryable<Category>> GetAllAsync()
        {
            try
            {
                return await Task.FromResult(_dbContext.Categories);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            try
            {
                Category? category = await _dbContext.Categories.FirstOrDefaultAsync(x=> x.CategoryId == id);
                return category;
            }
            catch (Exception ex)
            {
                return new Category();
            }
        }

        public async Task<bool> UpdateAsync(Category entity)
        {
            try
            {
                _dbContext.Categories.Update(entity);
                var res = await _dbContext.SaveChangesAsync();
                if (res != 0)
                    return true;
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
