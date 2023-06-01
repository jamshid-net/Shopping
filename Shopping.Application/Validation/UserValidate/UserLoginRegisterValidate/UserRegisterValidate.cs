using FluentValidation;
using Shopping.Application.DTO.UserDto.LoginRegister;
using UserRegister = Shopping.Application.DTO.UserDto.LoginRegister.UserRegister;

namespace Shopping.Application.Validation.UserValidate.UserLoginRegisterValidate
{
    public class UserRegisterValidate : AbstractValidator<UserRegister>
    {
        public UserRegisterValidate()
        {
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.UserName).NotEmpty().NotNull();
            RuleFor(x => x.Password)
            .MinimumLength(8).WithMessage("Minumum length 8")
            .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
            .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
            .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.");
            RuleFor(x => x.PasswordConfirm).NotEmpty().WithMessage("Please enter the confirmation password");
            RuleFor(x => x).Custom((x, context) =>
            {
                if (x.Password != x.PasswordConfirm)
                {
                    context.AddFailure(nameof(x.Password), "Passwords should match");
                }
            });

        }
    }
}
