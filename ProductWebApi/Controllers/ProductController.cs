using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopping.Application.Abstraction;
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
        public async Task<IActionResult> CreateProductAsync([FromQuery]Product product)
        {
            if(!ModelState.IsValid)
            {
               return BadRequest(ModelState);
            }
            await _productService.CreateAsync(product);
            _logger.LogInformation("Created product from employee" +User.FindFirstValue(ClaimTypes.Email));
            return Ok(product);
        }
        [Authorize(Roles = "Jamshid")]
        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProductAsync(Product product)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            var IsAdded= await _productService.UpdateAsync(product);
            if(IsAdded) return Ok(product);
            return BadRequest();
            
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
        [Authorize(Roles = "Jamshid")]
        [HttpPost("AddProducts")] 
        public async Task<IActionResult> CreateProductsAsync([FromBody] IEnumerable<Product> products)
        {
            List<IActionResult> result = new List<IActionResult>(); 
            foreach(var product in products)
            {
               result.Add (await CreateProductAsync(product));
            }
            return Ok(result);
        }

    }
}
