namespace UnitConversion.Common.Constants;

/// <summary>
/// User-facing conversion error messages shared across Domain and Application layers.
/// </summary>
public static class ConversionMessages
{
    public const string UnitsMustDiffer = "Source and target units must be different.";

    public static string UnitNotSupported(string unitCode) =>
        $"Unit '{unitCode}' is not supported.";

    public static string IncompatibleCategories(string fromUnit, string toUnit) =>
        $"Cannot convert between '{fromUnit}' and '{toUnit}' because they belong to different categories.";
}
