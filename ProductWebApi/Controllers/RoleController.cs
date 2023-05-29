using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopping.Application.Interfaces;
using Shopping.Application.Service;
using Shopping.Domain.Models;
using System.Data;

namespace ProductWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Jamshid")]
    public class RoleController : ApiBaseController
    {


        

       
        [HttpGet("Roles")]
        public async Task<IActionResult> GetAllRolesAsync()
        {
            var roles = await _roleService.GetAllAsync();
            return Ok(roles);
        }


        [HttpPost("AddRole")]
        public async Task<IActionResult> CreateRoleAsync([FromForm] Role role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _roleService.CreateAsync(role);
            return Ok(role);
        }
        [HttpPut("UpdateRole")]
        public async Task<IActionResult> UpdateRoleAsync([FromForm] Role role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var IsAdded = await _roleService.UpdateAsync(role);
            if (IsAdded) return Ok(role);
            return BadRequest();

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoleAsync(int id)
        {
            var isDeleted = await _roleService.DeleteAsync(id);
            if (isDeleted)
                return Ok();
            return NotFound();
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetByIdRoleAsync(int id)
        {
            if (id == 0)
                return BadRequest();
            var result = await _roleService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost("AddRoles")]
        public async Task<IActionResult> CreateRolesAsync([FromBody] IEnumerable<Role> roles)
        {
            List<IActionResult> result = new List<IActionResult>();
            foreach (var role in roles)
            {
                result.Add(await CreateRoleAsync(role));
            }
            return Ok(result);
        }
    }
}
