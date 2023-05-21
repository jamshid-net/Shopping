using Microsoft.EntityFrameworkCore;
using Shopping.Application.Abstraction;
using Shopping.Application.Interfaces;
using Shopping.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Application.Service
{
    public class PermissionService : IPermissionService
    {
        private readonly IApplicationDbContext _dbContext;

        public PermissionService(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> CreateAsync(Permission entity)
        {
            _dbContext.Permissions.Add(entity);
            int rowsAffected = await _dbContext.SaveChangesAsync();
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _dbContext.Permissions.FindAsync(id);
            if (entity == null)
                return false;

            _dbContext.Permissions.Remove(entity);
            int rowsAffected = await _dbContext.SaveChangesAsync();
            return rowsAffected > 0;
        }

        public Task<IQueryable<Permission>> GetAllAsync()
        {
            return Task.FromResult<IQueryable<Permission>>(_dbContext.Permissions);
        }

        public async Task<Permission> GetByIdAsync(int id)
        {
            var result  = await _dbContext.Permissions.FindAsync(id);
            if (result == null) return new Permission();
            return result;
        }

        public async Task<bool> UpdateAsync(Permission entity)
        {
            _dbContext.Permissions.Update(entity);
            int rowsAffected = await _dbContext.SaveChangesAsync();
            return rowsAffected > 0;
        }

    }
}
