using FluentValidation;
using Shopping.Application.DTO.RoleDto;

namespace Shopping.Application.Validation.RoleValidate
{
    public class RoleUpdateValidate:AbstractValidator<RoleUpdate>
    {
        public RoleUpdateValidate()
        {
            RuleFor(x => x.RoleId).NotNull();
            RuleFor(x => x.RoleName).NotNull().NotEmpty();
        }
    }
}
