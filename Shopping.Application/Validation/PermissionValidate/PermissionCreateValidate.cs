using FluentValidation;
using Shopping.Application.DTO.PermissionDto;

namespace Shopping.Application.Validation.PermissionValidate
{
    public class PermissionCreateValidate:AbstractValidator<PermissionCreate>
    {
        public PermissionCreateValidate()
        {
            RuleFor(x => x.PermissionName).NotNull().NotEmpty();
        }
    }
}
