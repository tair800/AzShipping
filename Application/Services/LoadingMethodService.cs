using AzShipping.Application.DTOs;
using AzShipping.Domain.Entities;
using AzShipping.Domain.Interfaces;

namespace AzShipping.Application.Services;

public class LoadingMethodService : ILoadingMethodService
{
    private readonly ILoadingMethodRepository _repository;

    public LoadingMethodService(ILoadingMethodRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<LoadingMethodDto>> GetAllAsync(string? languageCode = null)
    {
        var loadingMethods = await _repository.GetAllAsync();
        return loadingMethods.Select(lm => MapToDto(lm, languageCode));
    }

    public async Task<LoadingMethodDto?> GetByIdAsync(Guid id, string? languageCode = null)
    {
        var loadingMethod = await _repository.GetByIdAsync(id);
        return loadingMethod != null ? MapToDto(loadingMethod, languageCode) : null;
    }

    public async Task<LoadingMethodDto> CreateAsync(CreateLoadingMethodDto createDto)
    {
        var loadingMethod = new LoadingMethod
        {
            Id = Guid.NewGuid(),
            IsActive = createDto.IsActive,
            CreatedAt = DateTime.UtcNow
        };

        // Add translations
        var translations = new List<LoadingMethodTranslation>();
        
        // Use Name as EN translation, or if EN is in translations, use that
        var enName = createDto.Translations.ContainsKey("EN") 
            ? createDto.Translations["EN"] 
            : createDto.Name;
        
        if (!string.IsNullOrWhiteSpace(enName))
        {
            translations.Add(new LoadingMethodTranslation
            {
                Id = Guid.NewGuid(),
                LoadingMethodId = loadingMethod.Id,
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
                translations.Add(new LoadingMethodTranslation
                {
                    Id = Guid.NewGuid(),
                    LoadingMethodId = loadingMethod.Id,
                    LanguageCode = translation.Key.ToUpper(),
                    Name = translation.Value
                });
            }
        }

        loadingMethod.Translations = translations;
        var created = await _repository.CreateAsync(loadingMethod);
        return MapToDto(created, null);
    }

    public async Task<LoadingMethodDto?> UpdateAsync(Guid id, UpdateLoadingMethodDto updateDto)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing == null)
            return null;

        // Create new translations list instead of clearing and modifying the existing collection
        var newTranslations = new List<LoadingMethodTranslation>();
        
        // Use Name as EN translation, or if EN is in translations, use that
        var enName = updateDto.Translations.ContainsKey("EN") 
            ? updateDto.Translations["EN"] 
            : (updateDto.Translations.ContainsKey("en") 
                ? updateDto.Translations["en"] 
                : updateDto.Name);
        
        if (!string.IsNullOrWhiteSpace(enName))
        {
            newTranslations.Add(new LoadingMethodTranslation
            {
                Id = Guid.NewGuid(),
                LoadingMethodId = existing.Id,
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
                newTranslations.Add(new LoadingMethodTranslation
                {
                    Id = Guid.NewGuid(),
                    LoadingMethodId = existing.Id,
                    LanguageCode = translation.Key.ToUpper(),
                    Name = translation.Value
                });
            }
        }

        // Assign new translations collection
        existing.Translations = newTranslations;
        existing.IsActive = updateDto.IsActive;
        existing.UpdatedAt = DateTime.UtcNow;

        var updated = await _repository.UpdateAsync(existing);
        return MapToDto(updated, null);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _repository.DeleteAsync(id);
    }

    private static LoadingMethodDto MapToDto(LoadingMethod loadingMethod, string? languageCode = null)
    {
        var translations = loadingMethod.Translations.ToDictionary(
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
                ?? string.Empty;
        }

        return new LoadingMethodDto
        {
            Id = loadingMethod.Id,
            Name = displayName,
            IsActive = loadingMethod.IsActive,
            Translations = translations,
            CreatedAt = loadingMethod.CreatedAt,
            UpdatedAt = loadingMethod.UpdatedAt
        };
    }
}

