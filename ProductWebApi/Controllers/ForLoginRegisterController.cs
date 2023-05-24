using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Shopping.Application.DTOs.UserDto;
using Shopping.Application.Interfaces;
using Shopping.Domain.Models;
using System.Security.Claims;

namespace ProductWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class ForLoginRegisterController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public ForLoginRegisterController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("[action]")]
        [AllowAnonymous]
        public async Task RegisterUser([FromForm] UserRegister userRegister)
        {
            if(!ModelState.IsValid)
            {
                 BadRequest();
                
            }

            var newUser =   _mapper.Map<User>(userRegister);
            newUser.CreatedBy = userRegister.UserName;
            newUser.CreatedAt = DateTime.Now.ToUniversalTime();
            
            bool result = await _userService.CreateAsync(newUser);
            if(result) 
            {
             Log.Fatal("Success registered user " + userRegister.UserName);
                var context = HttpContext;
                context.Response.ContentType = "text/html";
                context.Response.Redirect("/productPage.html");
            }

        }

        #region LoginWithJwt
        //[HttpPost]
        //[Route("[action]")]
        //[AllowAnonymous]

        //public async Task<IActionResult> LoginUser([FromForm] UserLogin user)
        //{

        //    if (!await _userTokenService.AuthenAsync(user))
        //        Unauthorized();

        //    int time = 5;
        //    if (int.TryParse(_configuration["JWT:RefreshTokenExpiresTime"], out int t))
        //    {
        //        time = t;
        //    }
        //    TokenResponseModel token = await _jwtService.CreateTokenAsync(user);
        //    var userRefreshToken = new UserRefreshToken
        //    {
        //        UserEmail = token.UserEmail,
        //        RefreshToken = token.RefreshToken,
        //        ExpiresTime = DateTime.UtcNow.AddMinutes(time)
        //    };
        //    await _userTokenService.UpdateUserRefreshTokens(userRefreshToken);


        //    Log.Fatal("SERILOG: User is logined " + user.Email);
        //    _logger.LogError("ILOGGER:  USER IS LOGINED " + user.Email);
        //    return Ok(token);
        //}
        #endregion

        #region RefreshTokenForJwt
        //[HttpPost]
        //[Route("[action]")]
        //[AllowAnonymous]
        //public async Task<IActionResult> RefreshToken(TokenResponseModel tokenResponseModel)
        //{
        //    var principal = _jwtService.GetPrincipalFromExpiredToken(tokenResponseModel.AccessToken);
        //    var email = principal.FindFirstValue(ClaimTypes.Email);
        //    var user = await _userService.GetAsync(x => x.Email == email);

        //    var userLogin = new UserLogin
        //    {
        //        Email = user.Email,
        //        Password = user.Password
        //    };
        //    var savedRefreshtoken =await _userTokenService.GetSavedRefreshTokens(email, tokenResponseModel.RefreshToken);
        //    if ( savedRefreshtoken.RefreshToken != tokenResponseModel.RefreshToken) //savedRefreshtoken ==null ||
        //    {
        //         Log.Fatal("INVALID ATTEMPT ERROR IN REFRESHTOKEN");
        //        return Unauthorized("Invalid attempt!");
        //    }
        //    if(savedRefreshtoken.ExpiresTime <  DateTime.UtcNow)
        //    {
        //        return Unauthorized("TIME IS EXPIRED");
        //    }

        //    TokenResponseModel newJwtToken = await _jwtService.CreateTokenAsync(userLogin);
        //    if (newJwtToken == null)
        //    {
        //        return Unauthorized("Invalid attempt!");
        //    }
        //    int time = 5;
        //    if (int.TryParse(_configuration["JWT:RefreshTokenExpiresTime"], out int t))
        //    {
        //        time = t;
        //    }
        //    UserRefreshToken obj = new UserRefreshToken
        //    {
        //        RefreshToken = newJwtToken.RefreshToken,
        //        UserEmail = email,
        //        ExpiresTime = DateTime.UtcNow.AddMinutes(time)
        //    };
        //    bool isDeleted = await  _userTokenService.DeleteUserRefreshTokens(email, tokenResponseModel.RefreshToken);
        //    if (isDeleted)
        //    {
        //        await _userTokenService.AddUserRefreshTokens(obj);

        //    }
        //    else return BadRequest();
        //     Log.Fatal("refreshed token  for user " + tokenResponseModel.UserEmail);
        //    return Ok(newJwtToken);
        //}
        #endregion

        #region CookieAuthorize
        [HttpPost]
        [Route("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginUser([FromForm] UserLogin user)
        {
            var foundUser = await _userService.GetAsync(x => x.Email == user.Email);
            var context = HttpContext;
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (foundUser.Email==null)
                return Unauthorized();

            var claims = new List<Claim>() { new Claim(ClaimTypes.Email, user.Email) };

            foreach (UserRole userRole in foundUser.UsersRoles)
            {
                foreach (RolePermission permission in userRole.Role.RolePermissions)
                {
                    claims.Add(new Claim(ClaimTypes.Role, permission.Permission.PermissionName));
                }
            }

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            return Redirect("/productPage.html");

        }
        #endregion

    }
}
