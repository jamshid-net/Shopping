using Shopping.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Application.Interfaces
{
    public interface IUserService: IRepository<User>
    {

        Task<User> GetAsync(Expression<Func<User, bool>> expression);
    }
}
