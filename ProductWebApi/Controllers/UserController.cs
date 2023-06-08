using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopping.Application.DTOs;
using Shopping.Application.DTOs.UserDto;
using Shopping.Domain.Models;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Security.Claims;

namespace ProductWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ApiBaseController
    {

        [HttpGet("Users")]
        [Authorize(Roles = "Jamshid")]

        public async Task<IActionResult> GetAllUsersAsync()
        {
           
            var users = await _userService.GetAllAsync();
            ResponseDto<IQueryable<User>> response = new ResponseDto<IQueryable<User>>()
            {
                StatusCode = 200,
                Result = users

            };

            return Ok(response);
        }

        [Authorize(Roles = "Jamshid")]
        [AllowAnonymous]
        [HttpPost("AddUser")]
        public async Task<IActionResult> CreateUserAsync([FromForm] UserRegister userRegister)
        {
            if (!ModelState.IsValid)
            {
                BadRequest();

            }

            User newUser = new User()
            {
                Email = userRegister.Email,
                Password = userRegister.Password,
                UserName = userRegister.UserName,
                CreatedBy =User.FindFirstValue(ClaimTypes.Email),
                CreatedAt = DateTime.Now.ToUniversalTime()

            };
            bool result = await _userService.CreateAsync(newUser);
            ResponseDto<UserRegister> response = new ResponseDto<UserRegister>()
            {
                StatusCode = 200,
                IsSuccess = result,
                Result = userRegister
            };
            return Ok(response);    


        }

        [Authorize(Roles = "Jamshid")]
        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUserAsync( [FromForm] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            user.LastModified = DateTime.UtcNow.ToUniversalTime();
            user.LastModifiedBy = ClaimTypes.Email;
            var IsAdded = await _userService.UpdateAsync(user);
            if (IsAdded) return Ok(user);
            return BadRequest();

        }

        [Authorize(Roles = "Jamshid")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            var isDeleted = await _userService.DeleteAsync(id);
            if (isDeleted)
                return Ok();
            return NotFound();
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetByIdUserAsync(int id)
        {
            var result = await _userService.GetByIdAsync(id);
            return Ok(result);
            
            
        }
        [HttpGet("GetWithExpression")]
        public async Task<IActionResult> GetWithExpression(string expressionString)
        {

            var parameter = Expression.Parameter(typeof(User), "x");
            var expression = DynamicExpressionParser
                .ParseLambda(new[] { parameter }, typeof(bool), expressionString) as Expression<Func<User, bool>>;
            var compiledExpression = expression.Compile();

            var user =await _userService.GetAsync(expression);


            
            return Ok(user);


        }


       
    }
}
