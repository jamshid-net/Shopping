using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Shopping.Application.Interfaces;
using Shopping.Application.Service;
using Shopping.Domain.Models;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace ProductWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)=> 
            _userService = userService;
        [HttpGet("Users")]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [Authorize(Roles = "Jamshid")]
        [HttpPost("AddUser")]
        public async Task<IActionResult> CreateUserAsync([FromForm] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _userService.CreateAsync(user);
            return Ok(user);
        }

        [Authorize(Roles = "Jamshid")]
        [HttpPatch("UpdateUser")]
        public async Task<IActionResult> UpdateUserAsync( [FromForm] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
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

            var user = await _userService.GetAsync(expression);


        
            return Ok(user);


        }


        [Authorize(Roles = "Jamshid")]
        [HttpPost("AddUsers")]
        public async Task<IActionResult> CreateUsersAsync([FromBody] IEnumerable<User> users)
        {
            List<IActionResult> result = new List<IActionResult>();
            foreach (var user in users)
            {
                result.Add(await CreateUserAsync(user));
            }
            return Ok(result);
        }
    }
}
