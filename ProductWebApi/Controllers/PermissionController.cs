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
    public class PermissionController : ApiBaseController
    {
        
        [HttpGet("Permissions")]
        public async Task<IActionResult> GetAllPermissionsAsync()
        {
            var permissions = await _permissionService.GetAllAsync();
            return Ok(permissions);
        }

        
        [HttpPost("AddPermission")]
        public async Task<IActionResult> CreatePermissionAsync([FromForm] Permission permission)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
          
            await _permissionService.CreateAsync(permission);
            return Ok(permission);
        }
        [HttpPut("UpdatePermission")]
        public async Task<IActionResult> UpdatePermissionAsync([FromForm] Permission permission)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var IsAdded = await _permissionService.UpdateAsync(permission);
            if (IsAdded) return Ok(permission);
            return BadRequest();

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePermissionAsync(int id)
        {
            var isDeleted = await _permissionService.DeleteAsync(id);
            if (isDeleted)
                return Ok();
            return NotFound();
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetByIdPermissionAsync(int id)
        {
            if (id == 0)
                return BadRequest();
            var result = await _permissionService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost("AddPermissions")]
        public async Task<IActionResult> CreatePermissionsAsync([FromBody] IEnumerable<Permission> permissions)
        {
            List<IActionResult> result = new List<IActionResult>();
            foreach (var permission in permissions)
            {
                result.Add(await CreatePermissionAsync(permission));
            }
            return Ok(result);
        }

    }
}
