using Shopping.Application.DTOs.UserDto;
using Shopping.Domain.Models;

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
