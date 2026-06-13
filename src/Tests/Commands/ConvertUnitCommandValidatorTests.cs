using UnitConversion.Application.Commands.ConvertUnit;

namespace UnitConversion.Tests.Commands;

public sealed class ConvertUnitCommandValidatorTests
{
    private readonly ConvertUnitCommandValidator _validator = new();

    [Fact]
    public async Task Validate_WhenFromUnitIsEmpty_ReturnsError()
    {
        var command = new ConvertUnitCommand(100, string.Empty, "kilometer");

        var result = await _validator.ValidateAsync(command);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error => error.PropertyName == nameof(ConvertUnitCommand.FromUnit));
    }

    [Fact]
    public async Task Validate_WhenUnitsAreSame_ReturnsError()
    {
        var command = new ConvertUnitCommand(100, "meter", "meter");

        var result = await _validator.ValidateAsync(command);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error =>
            error.ErrorMessage == "Source and target units must be different.");
    }
}
