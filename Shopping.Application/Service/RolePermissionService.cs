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
    public class RolePermissionService : IRolePermissionService
    {
        private readonly IApplicationDbContext _dbContext;

        public RolePermissionService(IApplicationDbContext dbContext) =>
            _dbContext = dbContext;


        public async Task<bool> CreateAsync(RolePermission entity)
        {
            _dbContext.RolePermissions.Add(entity);
            int rowsAffected = await _dbContext.SaveChangesAsync();
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _dbContext.RolePermissions.FindAsync(id);
            if (entity == null)
                return false;

            _dbContext.RolePermissions.Remove(entity);
            int rowsAffected = await _dbContext.SaveChangesAsync();
            return rowsAffected > 0;
        }

        public Task<IQueryable<RolePermission>> GetAllAsync()
        {
            return Task.FromResult<IQueryable<RolePermission>>(_dbContext.RolePermissions.Include(x=>x.Permission).Include(x=> x.Role));
        }

        public async Task<RolePermission> GetByIdAsync(int id)
        {
            var result = await _dbContext.RolePermissions.Include(x=>x.Role).Include(x=>x.Permission).FirstOrDefaultAsync(x=> x.Id==id);
            if (result == null)
                return new RolePermission();
            return result;
        }

        public async Task<bool> UpdateAsync(RolePermission entity)
        {
            _dbContext.RolePermissions.Update(entity);
            int rowsAffected = await _dbContext.SaveChangesAsync();
            return rowsAffected > 0;
        }

    }
}
