using FluentValidation;
using Shopping.Application.DTO.OrderDto;

namespace Shopping.Application.Validation.OrderValidate
{
    public class OrderCreateValidate : AbstractValidator<OrderCreate>
    {
        public OrderCreateValidate()
        {
            RuleFor(x => x.UserId).NotNull();
            RuleFor(x=> x.Products).NotNull();
        }
    }
}
