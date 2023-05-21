using Shopping.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Application.Interfaces
{
    public interface IUserTokenService
    {
        Task<bool> AuthenAsync(UserLogin user);
        Task<UserRefreshToken> AddUserRefreshTokens(UserRefreshToken user);

        Task<UserRefreshToken> UpdateUserRefreshTokens(UserRefreshToken user);

       Task<UserRefreshToken> GetSavedRefreshTokens(string email, string refreshtoken);
       

       Task<bool> DeleteUserRefreshTokens(string email, string refreshToken);

      
    }
}
