using Shopping.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Application.Interfaces
{
    public interface IOrderRepository: IRepository<Order>
    {
        Task<Order> GetAsync(Expression<Func<Order, bool>> expression);
        Task<Order> AddOrderAsync(Order order);
    }
}
