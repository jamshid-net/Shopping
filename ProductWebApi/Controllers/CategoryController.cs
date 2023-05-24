using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopping.Application.DTOs;
using Shopping.Application.DTOs.CategoryDto;
using Shopping.Application.Interfaces;
using Shopping.Domain.Models;
using System.Security.Claims;
using Shopping.Application;
using AutoMapper;

namespace ProductWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryService categoryService,IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

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
        public async Task<IActionResult> AddCategoryAsync([FromForm] CategoryAdd category)
        {
            if (!ModelState.IsValid)
                return BadRequest();


            var newCategory = _mapper.Map<Category>(category);
            newCategory.CreatedAt = DateTime.Now.ToUniversalTime();
            newCategory.CreatedBy = ClaimTypes.Email;
           


           bool isAdded=   await _categoryService.CreateAsync(newCategory);
            ResponseDto<CategoryAdd> responseDto = new()
            {
                StatusCode = 200,
                IsSuccess = isAdded,
                Result = category
            };
            return Ok(responseDto);
        }
        [Authorize(Roles = "Jamshid")]
        [HttpPut]
        [Route("updatecategory")]
        public async Task<IActionResult> UpdateCategoryAsync([FromBody]CategoryUpdate category)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var newCategory = _mapper.Map<Category>(category);
            newCategory.LastModified = DateTime.Now.ToUniversalTime();  
            newCategory.LastModifiedBy = ClaimTypes.Email;
           
            var isUpdate = await _categoryService.UpdateAsync(newCategory);

            ResponseDto<CategoryUpdate> responseDto = new()
            {
                StatusCode = 200,
                IsSuccess = isUpdate,
                Result = category
            };
            return Ok(responseDto);

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
