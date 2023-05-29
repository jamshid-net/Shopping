using Shopping.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Application.Interfaces
{
    public interface ICartItemService :IRepository<CartItem>
    {
        Task<bool> DeleteAsyncExpression(Expression<Func<CartItem, bool>> expression);  
    }
}
