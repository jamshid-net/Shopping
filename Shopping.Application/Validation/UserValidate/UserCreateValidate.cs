using FluentValidation;
using Shopping.Application.DTO.UserDto;

namespace Shopping.Application.Validation.UserValidate
{
    public class UserCreateValidate : AbstractValidator<UserCreate>
    {
        public UserCreateValidate()
        {
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.UserName).NotEmpty().NotNull();
            RuleFor(x => x.Password)
            .MinimumLength(8).WithMessage("Minumum length 8")
            .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
            .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
            .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.");
           
        }
    }
}
