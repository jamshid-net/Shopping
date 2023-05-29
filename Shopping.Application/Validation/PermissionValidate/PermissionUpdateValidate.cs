using FluentValidation;
using Shopping.Application.DTO.PermissionDto;

namespace Shopping.Application.Validation.PermissionValidate
{
    public class PermissionUpdateValidate:AbstractValidator<PermissionUpdate>
    {
        public PermissionUpdateValidate()
        {
            RuleFor(x => x.PermissionId).NotNull();
            RuleFor(x => x.PermissionName).NotNull().NotEmpty();
        }
    }
}
