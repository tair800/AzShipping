using Request.Application.DTOs.Request;
using Request.Domain.AggregatesModel.RequestAggregate;

namespace Request.Application.Validation;

/// <summary>
/// Validates expected order details (dimensions) against request type.
/// Sea FCL/Breakbulk: Quantity + PackageType (dropdown format).
/// Sea LCL / Air: Length, Width, Height (Fill Dimensions format) or manual totals.
/// </summary>
public static class RequestDimensionValidator
{
    public static void Validate(IReadOnlyList<CreateRequestDimensionDto>? dimensions, RequestType? requestType)
    {
        if (requestType == null) return;

        var mode = (requestType.Mode ?? "").Trim();
        var subType = (requestType.SubType ?? "").Trim();

        var isFclOrBreakbulk = (string.Equals(mode, "Sea", StringComparison.OrdinalIgnoreCase)
                && (string.Equals(subType, "FCL", StringComparison.OrdinalIgnoreCase) || string.Equals(subType, "Breakbulk", StringComparison.OrdinalIgnoreCase)))
            || (string.Equals(mode, "Road", StringComparison.OrdinalIgnoreCase) && (string.Equals(subType, "FTL", StringComparison.OrdinalIgnoreCase) || string.Equals(subType, "Breakbulk", StringComparison.OrdinalIgnoreCase)))
            || (string.Equals(mode, "Rail", StringComparison.OrdinalIgnoreCase) && string.Equals(subType, "FCL", StringComparison.OrdinalIgnoreCase));

        if (isFclOrBreakbulk)
        {
            ValidateFclOrBreakbulk(dimensions);
        }
        else
        {
            ValidateLclOrAir(dimensions);
        }
    }

    private static void ValidateFclOrBreakbulk(IReadOnlyList<CreateRequestDimensionDto>? dimensions)
    {
        if (dimensions == null || dimensions.Count == 0)
            throw new ArgumentException("Expected order details (Quantity and Package type) are required for Sea FCL/Breakbulk, Road FTL/Breakbulk, and Rail FCL.");

        for (var i = 0; i < dimensions.Count; i++)
        {
            var d = dimensions[i];
            if (d.Quantity <= 0)
                throw new ArgumentException($"Dimension row {i + 1}: Quantity must be greater than 0 for Sea FCL/Breakbulk, Road FTL/Breakbulk, and Rail FCL.");
            if (string.IsNullOrWhiteSpace(d.PackageType))
                throw new ArgumentException($"Dimension row {i + 1}: Package type is required for Sea FCL/Breakbulk, Road FTL/Breakbulk, and Rail FCL.");
        }
    }

    private static void ValidateLclOrAir(IReadOnlyList<CreateRequestDimensionDto>? dimensions)
    {
        if (dimensions == null || dimensions.Count == 0) return;

        for (var i = 0; i < dimensions.Count; i++)
        {
            var d = dimensions[i];
            if (d.Length <= 0 || d.Width <= 0 || d.Height <= 0)
                throw new ArgumentException(
                    $"Dimension row {i + 1}: Length, Width and Height must be greater than 0 for Sea LCL and Air (Fill Dimensions format).");
        }
    }
}
