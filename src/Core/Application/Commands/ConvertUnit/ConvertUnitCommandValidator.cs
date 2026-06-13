using FluentValidation;

namespace UnitConversion.Application.Commands.ConvertUnit;

public sealed class ConvertUnitCommandValidator : AbstractValidator<ConvertUnitCommand>
{
    public ConvertUnitCommandValidator()
    {
        RuleFor(command => command.FromUnit)
            .NotEmpty()
            .MaximumLength(20);

        RuleFor(command => command.ToUnit)
            .NotEmpty()
            .MaximumLength(20);

        RuleFor(command => command)
            .Must(command => !string.Equals(command.FromUnit, command.ToUnit, StringComparison.OrdinalIgnoreCase))
            .WithMessage("Source and target units must be different.");
    }
}
