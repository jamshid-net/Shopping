using FluentValidation;
using Shopping.Application.DTO.CategoryDto;


namespace Shopping.Application.Validation.CategoryValidate
{
    public class CategoryUpdateValidate:AbstractValidator<CategoryUpdate>
    {
        public CategoryUpdateValidate()
        {
            RuleFor(x => x.CategoryId).NotNull();
            RuleFor(x => x.CategoryName).NotEmpty().NotNull();
        }
    }
}
