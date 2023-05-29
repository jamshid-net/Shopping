using Flurl.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using Serilog;
using Shopping.Application.DTOs;
using Shopping.Application.DTOs.ProductDto;
using Shopping.Application.Interfaces;
using Shopping.Domain.Models;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace ProductWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ApiBaseController
    {
        

        

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
            var products = (await _productService.GetAllAsync()).Skip((page - 1) * 6).Take(6);
            return Ok(products);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> SearchProductsAndGetAllAsync(string word)
        {
            var products = await _productService.GetAllAsync();
            var FoundProducts = new List<Product>();
            foreach (var product in products)
            {
                if (Regex.IsMatch(product.ProductName, $@"\w*{word}*", RegexOptions.IgnoreCase))
                {
                    FoundProducts.Add(product);
                }

            }
            return Ok(FoundProducts);
        }


        [Authorize(Roles = "Jamshid")]
        [HttpPost("AddProduct")]
        public async Task<IActionResult> CreateProductAsync([FromForm] ProductAdd product)
        {

            if(!ModelState.IsValid)
            {
               return BadRequest(ModelState.ToJson(formatting:Newtonsoft.Json.Formatting.Indented));
            }

            Product newProduct = product;
            newProduct.CreatedBy = User.FindFirstValue(ClaimTypes.Email);
            
            newProduct.CreatedAt = DateTime.Now.ToUniversalTime();

           bool isAdded= await _productService.CreateAsync(newProduct);
            Log.Information("Created product from employee" +User.FindFirstValue(ClaimTypes.Email));
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
                LastModified = DateTime.Now.ToUniversalTime(),
                LastModifiedBy = User.FindFirstValue(ClaimTypes.Email)
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
