using FluentValidation;
using UnitConversion.Application.Commands.ConvertUnit;
using UnitConversion.Common.Constants;

namespace UnitConversion.Application.Validators;

public sealed class ConvertUnitCommandValidator : AbstractValidator<ConvertUnitCommand>
{
    public ConvertUnitCommandValidator()
    {
        RuleFor(command => command.FromUnit)
            .NotEmpty()
            .MaximumLength(ValidationLimits.UnitCodeMaxLength);

        RuleFor(command => command.ToUnit)
            .NotEmpty()
            .MaximumLength(ValidationLimits.UnitCodeMaxLength);

        RuleFor(command => command)
            .Must(command => !string.Equals(command.FromUnit, command.ToUnit, StringComparison.OrdinalIgnoreCase))
            .WithMessage(ConversionMessages.UnitsMustDiffer);
    }
}
