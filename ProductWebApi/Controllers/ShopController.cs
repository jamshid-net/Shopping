using Microsoft.AspNetCore.Mvc;
using Shopping.Application.Abstraction;
using Shopping.Application.Interfaces;
using Shopping.Domain.Models;
using System.Security.Claims;

namespace ProductWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : Controller
    {

        private readonly IUserService _userService;
        private readonly IOrderRepository _orderRepository;

        public ShopController(IUserService userService, IOrderRepository orderRepository)
        {
            _userService = userService;
            _orderRepository = orderRepository;
        }


        [HttpPost("shop")]
        public async Task<IActionResult> Shop(CartItem cartItem)
        {

            string email = User.FindFirstValue(ClaimTypes.Email);
            User? user = await _userService.GetAsync(x => x.Email == email);
            if (user == null) return NotFound("User not found!");
            Order order = new()
            {
                User = user,
                CreatedAt = cartItem.DateCreated,
                UserId = user.UserId
            };
        
                var addedOrder = await _orderRepository.GetAsync(x => x.User == order.User);
                for (int i = 0; i < cartItem.Quantity; i++)
                {
                    addedOrder.OrderProducts.Add(new OrderProduct()
                    {
                        OrderId = addedOrder.OrderId,
                        ProductId = cartItem.ProductId

                    });


                }
            bool result =  await _orderRepository.CreateAsync(order);
            if(result) 
            return Ok(addedOrder);
            else
            return BadRequest();




        }
    }
}
