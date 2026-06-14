using FluentValidation;
using UnitConversion.Application.Commands.AuthenticateUser;

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
                .MaximumLength(50);

            RuleFor(command => command.Password)
                .NotEmpty()
                .MaximumLength(100);
        });
    }
}
