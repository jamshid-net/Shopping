namespace Shopping.Application.Service
{
    public class OrderService:IOrderRepository
    {
        private readonly IApplicationDbContext _dbContext;

        public OrderService(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
           
        }

        public async Task<Order> AddOrderAsync(Order order)
        {

            var addedorder = await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();
            foreach (var product in order.Products)
            {
                   await _dbContext.OrderProducts.AddAsync(new OrderProduct()
                   {
                       ProductId = product.ProductId,
                       OrderId = addedorder.Entity.OrderId
                   });
                    await _dbContext.SaveChangesAsync();
            }
            


            return addedorder.Entity;
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
            var orders = _dbContext.Orders.Include(x => x.User)
                .Include(x => x.OrderProducts).ThenInclude(x => x.Product);
            return await Task.FromResult(orders);
                

        }

        public async Task<Order> GetAsync(Expression<Func<Order, bool>> expression)
        {
            var order = _dbContext.Orders.Where(expression)?
                .Include(x => x.User)
                .Include(x => x.OrderProducts)
                .ThenInclude(x => x.Product)
                .FirstOrDefault();
            return await Task.FromResult(order);
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            var order = await _dbContext.Orders.Include(x => x.User)
                .FirstOrDefaultAsync(x=>x.OrderId == id);
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
