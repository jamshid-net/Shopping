namespace Shopping.Application.Service
{

    public class ProductService : IProductService
    {
        private readonly IApplicationDbContext _dbContext;

        public ProductService(IApplicationDbContext dbContext) =>
            _dbContext = dbContext;


        public async Task<bool> CreateAsync(Product entity)
        {
           
            _dbContext.Products.Add(entity);
            var result = await _dbContext.SaveChangesAsync();
            if (result != 0)
                return true;
            return false;

        }

        public async Task<bool> DeleteAsync(int id)
        {
            Product? entity = _dbContext.Products.FirstOrDefault(x => x.ProductId == id);
            _dbContext.Products.Remove(entity);
            var result = await _dbContext.SaveChangesAsync();
            if (result != 0)
                return true;
            return false;

        }

        public async Task<IQueryable<Product>> GetAllAsync()
        {
            return await Task.FromResult(_dbContext.Products.Include(x=> x.Category));
        }

        public async Task<Product> GetByIdAsync(int Id)
        {
            Product? product = await _dbContext.Products.Include(x=> x.Category).FirstOrDefaultAsync(x => x.ProductId == Id);
            return product;
        }



        public async Task<bool> UpdateAsync(Product entity)
        {
            var updated = _dbContext.Products.Update(entity);

            var result = await _dbContext.SaveChangesAsync();
            if (result != 0)
                return true;
            return false;


        }
    }
}
