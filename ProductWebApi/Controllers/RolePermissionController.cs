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
    public class RolePermissionController : ControllerBase
    {
        private readonly IRolePermissionService _rolePermissionService;

        public RolePermissionController(IRolePermissionService rolePermissionService)=>
            _rolePermissionService = rolePermissionService;
        [HttpGet("RolePermissions")]
        public async Task<IActionResult> GetAllRolePermissionsAsync()
        {
            var rolePermissions = await _rolePermissionService.GetAllAsync();
            return Ok(rolePermissions);
        }


        [HttpPost("AddRolePermission")]
        public async Task<IActionResult> CreateRolePermissionAsync([FromForm] RolePermission rolePermission)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _rolePermissionService.CreateAsync(rolePermission);
            return Ok(rolePermission);
        }
        [HttpPut("UpdateRolePermission")]
        public async Task<IActionResult> UpdateRolePermissionAsync([FromForm] RolePermission rolePermission)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var IsAdded = await _rolePermissionService.UpdateAsync(rolePermission);
            if (IsAdded) return Ok(rolePermission);
            return BadRequest();

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRolePermissionAsync(int id)
        {
            var isDeleted = await _rolePermissionService.DeleteAsync(id);
            if (isDeleted)
                return Ok();
            return NotFound();
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetByIdRolePermissionAsync(int id)
        {
            if (id == 0)
                return BadRequest();
            var result = await _rolePermissionService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost("AddRolePermissions")]
        public async Task<IActionResult> CreateRolePermissionsAsync([FromBody] IEnumerable<RolePermission> rolePermissions)
        {
            List<IActionResult> result = new List<IActionResult>();
            foreach (var role in rolePermissions)
            {
                result.Add(await CreateRolePermissionAsync(role));
            }
            return Ok(result);
        }

    }
}
