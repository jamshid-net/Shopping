using Shopping.Application.DTOs.TokenResponse;
using Shopping.Application.DTOs.UserDto;
using Shopping.Domain.Models;
using System.Security.Claims;

namespace Shopping.Application.Interfaces
{
    public interface IJwtService
    {
        

        public Task<TokenResponseModel> CreateTokenAsync(UserLogin user);

        Task<string> GenerateRefreshTokenAsync(UserLogin user);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);

    }
}
