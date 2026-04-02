using AzShipping.Application.DTOs;
using AzShipping.Domain.Entities;
using AzShipping.Domain.Interfaces;

namespace AzShipping.Application.Services;

public class TransportTypeService : ITransportTypeService
{
    private readonly ITransportTypeRepository _repository;

    public TransportTypeService(ITransportTypeRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<TransportTypeDto>> GetAllAsync(string? languageCode = null)
    {
        var transportTypes = await _repository.GetAllAsync();
        return transportTypes.Select(t => MapToDto(t, languageCode));
    }

    public async Task<TransportTypeDto?> GetByIdAsync(Guid id, string? languageCode = null)
    {
        var transportType = await _repository.GetByIdAsync(id);
        return transportType != null ? MapToDto(transportType, languageCode) : null;
    }

    public async Task<TransportTypeDto> CreateAsync(CreateTransportTypeDto createDto)
    {
        var transportType = new TransportType
        {
            Id = Guid.NewGuid(),
            Name = createDto.Name,
            IsAir = createDto.IsAir,
            IsSea = createDto.IsSea,
            IsRoad = createDto.IsRoad,
            IsRail = createDto.IsRail,
            IsActive = createDto.IsActive,
            CreatedAt = DateTime.UtcNow
        };

        // Add translations
        var translations = new List<TransportTypeTranslation>();
        
        // Use Name as EN translation, or if EN is in translations, use that
        var enName = createDto.Translations.ContainsKey("EN") 
            ? createDto.Translations["EN"] 
            : createDto.Name;
        
        if (!string.IsNullOrWhiteSpace(enName))
        {
            translations.Add(new TransportTypeTranslation
            {
                Id = Guid.NewGuid(),
                TransportTypeId = transportType.Id,
                LanguageCode = "EN",
                Name = enName
            });
        }

        // Add other translations (skip EN if already added)
        foreach (var translation in createDto.Translations)
        {
            if (translation.Key.ToUpper() == "EN") continue; // Already added
            
            if (!string.IsNullOrWhiteSpace(translation.Value))
            {
                translations.Add(new TransportTypeTranslation
                {
                    Id = Guid.NewGuid(),
                    TransportTypeId = transportType.Id,
                    LanguageCode = translation.Key.ToUpper(),
                    Name = translation.Value
                });
            }
        }

        transportType.Translations = translations;
        var created = await _repository.CreateAsync(transportType);
        return MapToDto(created, null);
    }

    public async Task<TransportTypeDto?> UpdateAsync(Guid id, UpdateTransportTypeDto updateDto)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing == null)
            return null;

        // Create new translations list instead of clearing and modifying the existing collection
        var newTranslations = new List<TransportTypeTranslation>();
        
        // Use Name as EN translation, or if EN is in translations, use that
        var enName = updateDto.Translations.ContainsKey("EN") 
            ? updateDto.Translations["EN"] 
            : (updateDto.Translations.ContainsKey("en") 
                ? updateDto.Translations["en"] 
                : updateDto.Name);
        
        if (!string.IsNullOrWhiteSpace(enName))
        {
            newTranslations.Add(new TransportTypeTranslation
            {
                Id = Guid.NewGuid(),
                TransportTypeId = existing.Id,
                LanguageCode = "EN",
                Name = enName
            });
        }

        // Add other translations (skip EN if already added)
        foreach (var translation in updateDto.Translations)
        {
            if (translation.Key.ToUpper() == "EN") continue; // Already added
            
            if (!string.IsNullOrWhiteSpace(translation.Value))
            {
                newTranslations.Add(new TransportTypeTranslation
                {
                    Id = Guid.NewGuid(),
                    TransportTypeId = existing.Id,
                    LanguageCode = translation.Key.ToUpper(),
                    Name = translation.Value
                });
            }
        }

        // Assign new translations collection
        existing.Translations = newTranslations;
        existing.Name = updateDto.Name;
        existing.IsAir = updateDto.IsAir;
        existing.IsSea = updateDto.IsSea;
        existing.IsRoad = updateDto.IsRoad;
        existing.IsRail = updateDto.IsRail;
        existing.IsActive = updateDto.IsActive;
        existing.UpdatedAt = DateTime.UtcNow;

        var updated = await _repository.UpdateAsync(existing);
        return MapToDto(updated, null);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _repository.DeleteAsync(id);
    }

    private static TransportTypeDto MapToDto(TransportType transportType, string? languageCode = null)
    {
        var translations = transportType.Translations.ToDictionary(
            t => t.LanguageCode,
            t => t.Name
        );

        // Get name based on requested language, fallback to EN, then first available
        string displayName;
        if (!string.IsNullOrWhiteSpace(languageCode) && translations.ContainsKey(languageCode.ToUpper()))
        {
            displayName = translations[languageCode.ToUpper()];
        }
        else
        {
            displayName = translations.GetValueOrDefault("EN") 
                ?? translations.Values.FirstOrDefault() 
                ?? transportType.Name
                ?? string.Empty;
        }

        return new TransportTypeDto
        {
            Id = transportType.Id,
            Name = displayName,
            IsAir = transportType.IsAir,
            IsSea = transportType.IsSea,
            IsRoad = transportType.IsRoad,
            IsRail = transportType.IsRail,
            IsActive = transportType.IsActive,
            Translations = translations,
            CreatedAt = transportType.CreatedAt,
            UpdatedAt = transportType.UpdatedAt
        };
    }
}

