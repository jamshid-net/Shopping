using Microsoft.EntityFrameworkCore;
using Shopping.Application.Abstraction;
using Shopping.Application.Interfaces;
using Shopping.Domain.Models;
using System.Linq.Expressions;

namespace Shopping.Application.Service
{
    public class UserService : IUserService
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IHashStringService _hashStringService;

        public UserService(IApplicationDbContext dbContext, IHashStringService hashStringService)
        {

            _dbContext = dbContext;
            _hashStringService = hashStringService;
        }

        public async Task<bool> CreateAsync(User entity)
        {
           string hashedPassword = await _hashStringService.HashStringAsync(entity.Password);
            List<UserRole> userRoles = new List<UserRole>();
            var AllRoles = _dbContext.Roles.ToList();
            entity.Password = hashedPassword;
            foreach (var roleId in entity.Roles)
            {
                if (AllRoles.Exists(x => x.RoleId == roleId))
                {
                    userRoles.Add(new UserRole()
                    {
                        Role = _dbContext.Roles.Find(roleId)
                    });
                }
            }

            entity.UsersRoles = userRoles;


            _dbContext.Users.Add(entity);
            int rowsAffected = await _dbContext.SaveChangesAsync();
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _dbContext.Users.FindAsync(id);
            if (entity == null)
                return false;

            _dbContext.Users.Remove(entity);
            int rowsAffected = await _dbContext.SaveChangesAsync();
            return rowsAffected > 0;
        }

        public async Task<IQueryable<User>> GetAllAsync()
        {

            var users = _dbContext.Users
                .Include(x => x.UsersRoles)
                .ThenInclude(x => x.Role)
                .ThenInclude(x=> x.RolePermissions)
                .ThenInclude(x=>x.Permission);
           
                
            return await Task.FromResult(users);
           
        }

        public async Task<User> GetAsync(Expression<Func<User,bool>> expression)
        {
            User? user = _dbContext.Users.Where(expression)?
                .Include(x=>x.UsersRoles)?
                .ThenInclude(x=>x.Role)?
                .ThenInclude(x=>x.RolePermissions)
                .ThenInclude(x=>x.Permission)
                .FirstOrDefault();

            return user;
        }

        public async Task<User> GetByIdAsync(int id)
        {

            var users = _dbContext.Users;
            var result = await users.FirstOrDefaultAsync(x=> x.UserId == id);
            if (result == null)
                return new User();
            return result;
        }

        public async Task<bool> UpdateAsync(User entity)
        {
            string hashedPassword = await _hashStringService.HashStringAsync(entity.Password);
            List<UserRole> userRoles = new List<UserRole>();
            var AllRoles = _dbContext.Roles.ToList();
            entity.Password = hashedPassword;
            foreach (var roleId in entity.Roles)
            {
                if (AllRoles.Exists(x => x.RoleId == roleId))
                {
                    userRoles.Add(new UserRole()
                    {
                        Role = _dbContext.Roles.Find(roleId)
                    });
                }
            }

            entity.UsersRoles = userRoles;


            _dbContext.Users.Update(entity);
            int rowsAffected = await _dbContext.SaveChangesAsync();
            return rowsAffected > 0;
        }

        

    }
}
