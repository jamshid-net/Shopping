using Microsoft.AspNetCore.Mvc;
using Shopping.Application.Abstraction;
using Shopping.Application.Interfaces;
using Shopping.Domain.Models;
using System.Security.Claims;

namespace ProductWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserService _userSerive;
        


        public ShopController(IOrderRepository orderRepository, IUserService userSerive)
        {
            _orderRepository = orderRepository;
            _userSerive = userSerive;
           
        }


        [HttpPost("shop")]
        public async Task<IActionResult> Shop(CartItem cartItem)
        {

            string email = User.FindFirstValue(ClaimTypes.Email);
            User? user = await _userSerive.GetAsync(x => x.Email == email);
            if (user == null) return NotFound("User not found!");
            Order order = new()
            {
                User = user,
                CreatedDate = cartItem.DateCreated,
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
