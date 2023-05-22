using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using Shopping.Application.Abstraction;
using Shopping.Application.DTOs;
using Shopping.Application.DTOs.ProductDto;
using Shopping.Application.Interfaces;
using Shopping.Domain.Models;
using System.Security.Claims;

namespace ProductWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;
        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }
        [HttpGet("Products")]
       // [Authorize(Roles = "GetAllProducts")]
        
        public async Task<IActionResult> GetAllProductsAsync()
        {
            var products = await _productService.GetAllAsync();
            return Ok(products);
        }
      
        [HttpGet("pagenation")]
        public async Task<IActionResult> GetAllProductsPageAsync(int page)
        {
            var products = (await _productService.GetAllAsync()).Skip((page - 1) * 5).Take(5);
            return Ok(products);
        }

        [Authorize(Roles = "Jamshid")]
        [HttpPost("AddProduct")]
        public async Task<IActionResult> CreateProductAsync([FromForm] ProductAdd product)
        {
            if(!ModelState.IsValid)
            {
               return BadRequest(ModelState.ToJson(formatting:Newtonsoft.Json.Formatting.Indented));
            }
            
            Product newProduct = new()      
            {
                CategoryId = product.CategoryId,
                ProductName = product.ProductName,
                Picture = product.Picture,
                Price = product.Price,
                Description = product.Description,

            };

           bool isAdded= await _productService.CreateAsync(newProduct);
            _logger.LogInformation("Created product from employee" +User.FindFirstValue(ClaimTypes.Email));
            ResponseDto<ProductAdd> response = new()
            {
                StatusCode = 200,
                IsSuccess = isAdded,
                Message = "Added succsessfully",
                Result = product

            };
            return Ok(response);
        }
        [Authorize(Roles = "Jamshid")]
        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProductAsync(ProductUpdate product)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            Product newProduct = new()
            {
                ProductId = product.ProductId,
                CategoryId = product.CategoryId,
                ProductName = product.ProductName,
                Picture = product.Picture,
                Price = product.Price,
                Description = product.Description,

            };
            var isUpdated = await _productService.UpdateAsync(newProduct);
            ResponseDto<ProductUpdate> response = new()
            {
                StatusCode = 200,
                IsSuccess = isUpdated,
                Message = "Updated succsessfully",
                Result = product

            };
            return Ok(response);
            
            
        }

        [Authorize(Roles ="Jamshid")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductAsync(int id)
        {
           var isDeleted=  await _productService.DeleteAsync(id);
            if(isDeleted) 
                return Ok();
            return NotFound();
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetByIdProductAsync(int id)
        {
            if(id==0) 
                return BadRequest();
            var result =await _productService.GetByIdAsync(id);
            return Ok(result);
        }
       

    }
}
