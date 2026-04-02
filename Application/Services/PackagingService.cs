using AzShipping.Application.DTOs;
using AzShipping.Domain.Entities;
using AzShipping.Domain.Interfaces;

namespace AzShipping.Application.Services;

public class PackagingService : IPackagingService
{
    private readonly IPackagingRepository _repository;

    public PackagingService(IPackagingRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<PackagingDto>> GetAllAsync(string? languageCode = null)
    {
        var packagings = await _repository.GetAllAsync();
        return packagings.Select(p => MapToDto(p, languageCode));
    }

    public async Task<PackagingDto?> GetByIdAsync(Guid id, string? languageCode = null)
    {
        var packaging = await _repository.GetByIdAsync(id);
        return packaging != null ? MapToDto(packaging, languageCode) : null;
    }

    public async Task<PackagingDto> CreateAsync(CreatePackagingDto createDto)
    {
        var packaging = new Packaging
        {
            Id = Guid.NewGuid(),
            IsActive = createDto.IsActive,
            CreatedAt = DateTime.UtcNow
        };

        // Add translations
        var translations = new List<PackagingTranslation>();
        
        // Use Name as EN translation, or if EN is in translations, use that
        var enName = createDto.Translations.ContainsKey("EN") 
            ? createDto.Translations["EN"] 
            : createDto.Name;
        
        if (!string.IsNullOrWhiteSpace(enName))
        {
            translations.Add(new PackagingTranslation
            {
                Id = Guid.NewGuid(),
                PackagingId = packaging.Id,
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
                translations.Add(new PackagingTranslation
                {
                    Id = Guid.NewGuid(),
                    PackagingId = packaging.Id,
                    LanguageCode = translation.Key.ToUpper(),
                    Name = translation.Value
                });
            }
        }

        packaging.Translations = translations;
        var created = await _repository.CreateAsync(packaging);
        return MapToDto(created, null);
    }

    public async Task<PackagingDto?> UpdateAsync(Guid id, UpdatePackagingDto updateDto)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing == null)
            return null;

        existing.IsActive = updateDto.IsActive;
        existing.UpdatedAt = DateTime.UtcNow;

        // Update translations
        existing.Translations.Clear();
        
        // Use Name as EN translation, or if EN is in translations, use that
        var enName = updateDto.Translations.ContainsKey("EN") 
            ? updateDto.Translations["EN"] 
            : updateDto.Name;
        
        if (!string.IsNullOrWhiteSpace(enName))
        {
            existing.Translations.Add(new PackagingTranslation
            {
                Id = Guid.NewGuid(),
                PackagingId = existing.Id,
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
                existing.Translations.Add(new PackagingTranslation
                {
                    Id = Guid.NewGuid(),
                    PackagingId = existing.Id,
                    LanguageCode = translation.Key.ToUpper(),
                    Name = translation.Value
                });
            }
        }

        var updated = await _repository.UpdateAsync(existing);
        return MapToDto(updated, null);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _repository.DeleteAsync(id);
    }

    private static PackagingDto MapToDto(Packaging packaging, string? languageCode = null)
    {
        var translations = packaging.Translations.ToDictionary(
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

        return new PackagingDto
        {
            Id = packaging.Id,
            Name = displayName,
            IsActive = packaging.IsActive,
            Translations = translations,
            CreatedAt = packaging.CreatedAt,
            UpdatedAt = packaging.UpdatedAt
        };
    }
}

