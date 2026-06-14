using FluentValidation;
using UnitConversion.Application.Commands.AuthenticateUser;
using UnitConversion.Common.Constants;
using UnitConversion.Common.Enums;

namespace UnitConversion.Application.Validators;

public sealed class AuthenticateUserCommandValidator : AbstractValidator<AuthenticateUserCommand>
{
    public AuthenticateUserCommandValidator()
    {
        RuleFor(command => command.GrantType)
            .IsInEnum();

        When(command => command.GrantType == GrantType.Password, () =>
        {
            RuleFor(command => command.Username)
                .NotEmpty()
                .MaximumLength(ValidationLimits.UsernameMaxLength);

            RuleFor(command => command.Password)
                .NotEmpty()
                .MaximumLength(ValidationLimits.PasswordMaxLength);
        });
    }
}
