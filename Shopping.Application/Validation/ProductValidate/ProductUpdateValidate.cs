using FluentValidation;
using Shopping.Application.DTO.ProductDto;

namespace Shopping.Application.Validation.ProductValidate
{
    public class ProductUpdateValidate : AbstractValidator<ProductUpdate>
    {
        public ProductUpdateValidate()
        {
            RuleFor(x => x.ProductId).NotNull();
            RuleFor(x => x.CategoryId).NotNull();
            RuleFor(x => x.ProductName).NotNull().NotEmpty();
            RuleFor(x => x.Price).NotNull();
           
        }
    }
}
