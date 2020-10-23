using FluentValidation;
using Services.DTOs;

namespace Services.Validators
{
    public class UserRegistrationRequestValidator : AbstractValidator<UserRegistrationDTO>
    {
        public UserRegistrationRequestValidator()
        {
            RuleFor(u => u.Email).NotEmpty().NotNull().EmailAddress();
            RuleFor(u => u.Password).NotEmpty().NotNull().MinimumLength(8);
            RuleFor(u => u.Name).NotEmpty().NotNull();
            RuleFor(u => u.District).NotEmpty().NotNull();
        }
    }
}