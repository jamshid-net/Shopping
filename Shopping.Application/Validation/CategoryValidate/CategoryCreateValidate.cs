using FluentValidation;
using Shopping.Application.DTO.CategoryDto;

namespace Shopping.Application.Validation.CategoryValidate
{
    public class CategoryCreateValidate :AbstractValidator<CategoryCreate>
    {
        public CategoryCreateValidate()
        {
            RuleFor(x => x.CategoryName).NotEmpty().NotNull();
        }
    }
}
