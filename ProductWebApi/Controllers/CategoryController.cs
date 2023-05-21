using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopping.Application.Interfaces;
using Shopping.Domain.Models;

namespace ProductWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService) => _categoryService = categoryService;

        [HttpGet]
        [Route("categories")]
        public async Task<IActionResult> GetAllCategoriesAsync()
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(categories);
        }

        [HttpGet]
        [Route("category/{id}")]
        public async Task<IActionResult> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            return Ok(category);
        }

        [HttpPost]
        [Route("addcategory")]
        public async Task<IActionResult> AddCategoryAsync([FromForm] Category category)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            await _categoryService.CreateAsync(category);
            return Ok(category);
        }
        [Authorize(Roles = "Jamshid")]
        [HttpPut]
        [Route("updatecategory")]
        public async Task<IActionResult> UpdateCategoryAsync([FromBody]Category category)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var isUpdate = await _categoryService.UpdateAsync(category);
            if (isUpdate)
                return Ok(category);
            return BadRequest();
        }
        [Authorize(Roles = "Jamshid")]
        [HttpDelete]
        [Route("deletecategory/{id}")]
        public async Task<IActionResult> DeleteCategoryAsync(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var isDeleted = await _categoryService.DeleteAsync(id);
            if (isDeleted) return Ok();
            return BadRequest();
        }
    }
}
