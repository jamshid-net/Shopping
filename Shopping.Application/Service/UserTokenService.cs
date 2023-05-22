using Microsoft.EntityFrameworkCore;
using Shopping.Application.Abstraction;
using Shopping.Application.DTOs.UserDto;
using Shopping.Application.Interfaces;
using Shopping.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Application.Service
{
    public class UserTokenService : IUserTokenService
    {
        private readonly IUserService _userService; 
        private readonly IHashStringService _hashStringService;
        private readonly IApplicationDbContext _dbContext;
        
        public UserTokenService(IUserService userService, IHashStringService hashStringService,IApplicationDbContext dbContext)
        {
            _userService = userService; 
            _dbContext = dbContext;
            _hashStringService = hashStringService;
        }
        public async Task<UserRefreshToken> AddUserRefreshTokens(UserRefreshToken user)
        {
            try
            {
                _dbContext.UserRefreshTokens.Add(user);
                await _dbContext.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                return null;
            }
           

        }

        public async Task<bool> AuthenAsync(UserLogin user)
        {
            string hashedPassword = await _hashStringService.HashStringAsync(user.Password);
            var result = _userService.GetAsync(x => x.Email == user.Email);
            if (result != null)
                return true;
            else return false;
        }

        public async Task<bool> DeleteUserRefreshTokens(string email, string refreshToken)
        {
            try
            {
                var userRefreshtone = _dbContext.UserRefreshTokens
                    .FirstOrDefault(x=>x.UserEmail==email && refreshToken == x.RefreshToken);
                _dbContext.UserRefreshTokens.Remove(userRefreshtone);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                
            }
        }

        public  Task<UserRefreshToken> GetSavedRefreshTokens(string username, string refreshtoken)
        {
           return  Task.FromResult(_dbContext.UserRefreshTokens
               .FirstOrDefault(x => x.UserEmail == username && x.RefreshToken == refreshtoken));
        }

        public async Task<UserRefreshToken> UpdateUserRefreshTokens(UserRefreshToken user)
        {
            var res =await _dbContext.UserRefreshTokens
                .FirstOrDefaultAsync(x=>x.UserEmail ==user.UserEmail);
            if (res == null)
            {
                await AddUserRefreshTokens(user);
                return user;
            }
            else
            {
                res.RefreshToken = user.RefreshToken;
                res.ExpiresTime = user.ExpiresTime;
                 _dbContext.UserRefreshTokens.Update(res);
                await _dbContext.SaveChangesAsync();
                return user;
            }
        }
    }
}
