using FluentValidation;
using Shopping.Application.DTO.ProductDto;

namespace Shopping.Application.Validation.ProductValidate
{
    public class ProductCreateValidate : AbstractValidator<ProductCreate>
    {
        public ProductCreateValidate()
        {
            RuleFor(x => x.CategoryId).NotNull();
            RuleFor(x => x.ProductName).NotNull().NotEmpty().WithMessage("Please enter product name");
            RuleFor(x => x.Price).NotNull();
            
        }
    }
}
