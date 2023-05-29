using FluentValidation;
using Shopping.Application.DTO.RoleDto;

namespace Shopping.Application.Validation.RoleValidate
{
    public class RoleCreateValidate:AbstractValidator<RoleCreate>
    {
        public RoleCreateValidate()
        {
            RuleFor(x => x.RoleName).NotNull().NotEmpty();
            
        }
    }
}
