namespace Shopping.Application.Service
{
    public class RoleService : IRoleService
    {
        private readonly IApplicationDbContext _dbContext;

        public RoleService(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> CreateAsync(Role entity)
        {
            _dbContext.Roles.Add(entity);
            int rowsAffected = await _dbContext.SaveChangesAsync();
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _dbContext.Roles.FindAsync(id);
            if (entity == null)
                return false;

            _dbContext.Roles.Remove(entity);
            int rowsAffected = await _dbContext.SaveChangesAsync();
            return rowsAffected > 0;
        }

        public Task<IQueryable<Role>> GetAllAsync()
        {
            return Task.FromResult<IQueryable<Role>>(_dbContext.Roles);
        }

        public async Task<Role> GetByIdAsync(int id)
        {
            var result = await _dbContext.Roles.FindAsync(id);
            if (result == null) return new Role();
            return result;
        }

        public async Task<bool> UpdateAsync(Role entity)
        {
            _dbContext.Roles.Update(entity);
            int rowsAffected = await _dbContext.SaveChangesAsync();
            return rowsAffected > 0;
        }
    }
}
