

namespace Shopping.Application.Service
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly IHashStringService _hashStringService;
        public JwtService(IConfiguration configuration, IUserService userService, IHashStringService hashStringService)
        {

            _configuration = configuration;
            _userService = userService;
            _hashStringService = hashStringService;
        }
      

        public async Task<TokenResponseModel> CreateTokenAsync(UserLogin user)
        {
            
            var foundUser =await _userService.GetAsync(x=> x.Email == user.Email);
            if (foundUser == null) return new TokenResponseModel();
            List<Claim> permissions = new List<Claim>()
            {
                new Claim(ClaimTypes.Email,user.Email)

            };
            foreach (var userRole in foundUser.Roles)
            {
                foreach (Permission permission in userRole.Permissions)
                {
                    permissions.Add(new Claim(ClaimTypes.Role, permission.PermissionName));
                }
            }

           
            int minute = 5;
           if(int.TryParse(_configuration["JWT:ExpiresInMinutes"], out int _minute))
           {
                minute = _minute;
           }
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                    issuer: _configuration["JWT:Issuer"],
                    audience: _configuration["JWT:Audience"],
                    claims: permissions,
                    expires: DateTime.UtcNow.AddMinutes(minute),
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey
                        (Encoding.UTF8.GetBytes(_configuration["JWT:Key"])),
                        SecurityAlgorithms.HmacSha256)
                );
            var result = new TokenResponseModel
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                RefreshToken = await GenerateRefreshTokenAsync(user),
                UserEmail = user.Email

            };
            return result; 

        }

        public async Task<string> GenerateRefreshTokenAsync(UserLogin userLogin)
        {
            string randomToken =await _hashStringService.HashStringAsync(userLogin.Email+DateTime.UtcNow.ToString());
            return randomToken;
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {

            
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidAudience = _configuration["JWT:Audience"],
                ValidIssuer = _configuration["JWT:Issuer"],
                ValidateLifetime = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"])),
                

            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }


            return principal;
        }
        
        
    }
}
