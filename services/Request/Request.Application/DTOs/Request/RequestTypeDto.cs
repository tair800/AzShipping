namespace Request.Application.DTOs.Request;

public record RequestTypeDto(
    Guid Id,
    string Code,
    string Name,
    string Direction,
    string Mode,
    string? SubType,
    string RequestNumberPrefix,
    string CarrierApiPath,
    string CarrierLabel,
    int SortOrder,
    bool IsActive,
    bool SupportsVas,
    /// <summary>Fill Dimensions modal title (e.g. "Fill Dimensions (Sea – Ocean 1 CBM = 1 MT)").</summary>
    string FillDimensionsTitle,
    /// <summary>Volumetric weight column label (e.g. "Volumetric Weight (MT)" or "Volumetric Weight (KG)").</summary>
    string FillDimensionsVolumetricWeightLabel,
    /// <summary>Tooltip for volumetric weight column (e.g. "Road 1 CBM = 1 MT. Volumetric weight = Volume × 1 MT").</summary>
    string FillDimensionsVolumetricWeightTooltip,
    /// <summary>Chargeable weight label (e.g. "Chargeable Weight (MT)" or "Chargeable Weight (KG)").</summary>
    string FillDimensionsChargeableWeightLabel);
