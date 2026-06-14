namespace UnitConversion.Common.Constants;

/// <summary>
/// Shared field length limits for validation (FluentValidation, future API models, DB columns).
/// </summary>
public static class ValidationLimits
{
    public const int UnitCodeMaxLength = 20;

    public const int UsernameMaxLength = 50;

    public const int PasswordMaxLength = 100;
}
