using FluentValidation;
using Shopping.Application.DTO.OrderDto;

namespace Shopping.Application.Validation.OrderValidate
{
    public class OrderUpdateValidate:AbstractValidator<OrderUpdate>
    {
        public OrderUpdateValidate()
        {
            RuleFor(x => x.OrderId).NotNull();
            RuleFor(x => x.UserId).NotNull();
            RuleFor(x => x.IsCompleted).NotNull();
            RuleFor(x => x.Products).NotNull();
        }
    }
}
