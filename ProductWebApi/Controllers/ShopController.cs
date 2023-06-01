using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters.Xml;
using Shopping.Application.Abstraction;
using Shopping.Application.DTOs.DtoForProfile;
using Shopping.Application.Interfaces;
using Shopping.Domain.Models;
using System.Security.Claims;

namespace ProductWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ShopController : ApiBaseController
    {


        [HttpGet("userCart")]
        public async Task<IActionResult> GetUserCart()
        {
            var allCart = await _cartItemService.GetAllAsync();
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userService.GetAsync(x => x.Email == userEmail);
            var userCart = allCart.Where(x => x.UserId == user.UserId);
            return Ok(userCart);

        }

        [HttpPost("addToOrder")]
        public async Task<IActionResult> AddToOrder()
        {
          
            var allCart = await _cartItemService.GetAllAsync();
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userService.GetAsync(x => x.Email == userEmail);
            var userCart = allCart.Where(x => x.UserId == user.UserId);
            var products = allCart.Select(x => x.Product).ToList();

            Order order = new Order()
            {
                CreatedAt = DateTime.Now.ToUniversalTime(),
                CreatedBy = userEmail,
                User = user,
                UserId = user.UserId,
                Products = products
                
            };
            var AddedOrder = await _orderRepository.AddOrderAsync(order);
            


            await _cartItemService.DeleteAsyncExpression(x => x.UserId == user.UserId);
            return Ok(AddedOrder);

        }

        [HttpGet("getUserOrders")]
        public async Task<IActionResult> GetUserOrder()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userService.GetAsync(x => x.Email == userEmail);
            var UserOrders =(await _orderRepository.GetAllAsync()).Where(x=>x.UserId == user.UserId);
            List<UserOrderDto> orders = new List<UserOrderDto>();   
            foreach (var userorder in UserOrders)
            {
                
                    orders.Add(new UserOrderDto()
                    {
                        orderId = userorder.OrderId,
                        OrderDate = userorder.CreatedAt,
                        TotalPrice = userorder.OrderProducts.Sum(x=>x.Product.Price),
                        IsDelivered = userorder.IsCompleted

                    });
                
            }
            return Ok(orders);

        }

        [HttpPost("addToCart")]
        public async Task<IActionResult> AddToCart([FromQuery] int productId)
        {
            var product = await _productService.GetByIdAsync(productId);
            if (product == null)
            {
                return NotFound();
            }
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userService.GetAsync(x => x.Email == userEmail);
            if (user == null)
            {
                return NotFound();
            }
            CartItem newCartItem = new()
            {
                Product = product,
                ProductId = product.ProductId,
                User = user,
                UserId = user.UserId
            };
           var result =  await _cartItemService.CreateAsync(newCartItem);
           if(result) return Ok(newCartItem);   
           else return BadRequest();    

        }
        [HttpGet("countOfItem")]
        public async Task<IActionResult> CountInCartItem()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userService.GetAsync(x => x.Email == userEmail);
            var AllCartItem = await _cartItemService.GetAllAsync();
            var Count = AllCartItem.Where(x => x.UserId == user.UserId).Count();
            return Ok(Count);
        }


        [HttpGet("totalPrice")]
        public async Task<IActionResult> TotalPrice()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userService.GetAsync(x => x.Email == userEmail);
            var AllCartItem = await _cartItemService.GetAllAsync();
            var sum = AllCartItem.Where(x => x.UserId == user.UserId).Sum(x=>x.Product.Price);
            return Ok(sum);

        }
        [HttpDelete("removeCartItem")]
        public async Task<IActionResult> RemoveCartItem([FromQuery] int id)
        {
            var result = await _cartItemService.DeleteAsync(id);
            return Ok(result);
        }
        [HttpGet("aboutuser")]
        public async Task<IActionResult> AboutUser()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userService.GetAsync(x => x.Email == userEmail);
            return Ok(user);
        }
    }
}
