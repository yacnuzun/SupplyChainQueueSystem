using AccountApi.Dto_s;
using FluentValidation;

namespace AccountApi.Application.Validators
{
    public class UserClaimValidator : AbstractValidator<UserClaimDto>
    {
        public UserClaimValidator()
        {
            RuleFor(c => c.Role).IsInEnum();
            RuleFor(u=> u.UserId).NotNull().GreaterThan(0);
        }
    }
}
