using AzShipping.Application.DTOs;
using AzShipping.Domain.Entities;
using AzShipping.Domain.Interfaces;

namespace AzShipping.Application.Services;

public class SalesFunnelStatusService : ISalesFunnelStatusService
{
    private readonly ISalesFunnelStatusRepository _repository;
    private readonly IUserRepository _userRepository;

    public SalesFunnelStatusService(ISalesFunnelStatusRepository repository, IUserRepository userRepository)
    {
        _repository = repository;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<SalesFunnelStatusDto>> GetAllAsync(string? languageCode = null)
    {
        var statuses = await _repository.GetAllAsync();
        return statuses.Select(s => MapToDto(s, languageCode));
    }

    public async Task<SalesFunnelStatusDto?> GetByIdAsync(Guid id, string? languageCode = null)
    {
        var status = await _repository.GetByIdAsync(id);
        return status != null ? MapToDto(status, languageCode) : null;
    }

    public async Task<SalesFunnelStatusDto> CreateAsync(CreateSalesFunnelStatusDto createDto)
    {
        var status = new SalesFunnelStatus
        {
            Id = Guid.NewGuid(),
            StatusPosition = createDto.StatusPosition,
            ResponsibleManagerId = createDto.ResponsibleManagerId,
            NumberOfDays = createDto.NumberOfDays,
            SendToEmail = createDto.SendToEmail,
            SendNotification = createDto.SendNotification,
            IsActive = createDto.IsActive,
            CreatedAt = DateTime.UtcNow
        };

        // Add translations
        var translations = new List<SalesFunnelStatusTranslation>();
        
        // Use Name as EN translation, or if EN is in translations, use that
        var enName = createDto.Translations.ContainsKey("EN") 
            ? createDto.Translations["EN"] 
            : createDto.Name;
        
        if (!string.IsNullOrWhiteSpace(enName))
        {
            translations.Add(new SalesFunnelStatusTranslation
            {
                Id = Guid.NewGuid(),
                SalesFunnelStatusId = status.Id,
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
                translations.Add(new SalesFunnelStatusTranslation
                {
                    Id = Guid.NewGuid(),
                    SalesFunnelStatusId = status.Id,
                    LanguageCode = translation.Key.ToUpper(),
                    Name = translation.Value
                });
            }
        }

        status.Translations = translations;
        var created = await _repository.CreateAsync(status);
        return MapToDto(created, null);
    }

    public async Task<SalesFunnelStatusDto?> UpdateAsync(Guid id, UpdateSalesFunnelStatusDto updateDto)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing == null)
            return null;

        // Create new translations list instead of clearing and modifying the existing collection
        var newTranslations = new List<SalesFunnelStatusTranslation>();
        
        // Use Name as EN translation, or if EN is in translations, use that
        var enName = updateDto.Translations.ContainsKey("EN") 
            ? updateDto.Translations["EN"] 
            : (updateDto.Translations.ContainsKey("en") 
                ? updateDto.Translations["en"] 
                : updateDto.Name);
        
        if (!string.IsNullOrWhiteSpace(enName))
        {
            newTranslations.Add(new SalesFunnelStatusTranslation
            {
                Id = Guid.NewGuid(),
                SalesFunnelStatusId = existing.Id,
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
                newTranslations.Add(new SalesFunnelStatusTranslation
                {
                    Id = Guid.NewGuid(),
                    SalesFunnelStatusId = existing.Id,
                    LanguageCode = translation.Key.ToUpper(),
                    Name = translation.Value
                });
            }
        }

        // Assign new translations collection
        existing.Translations = newTranslations;

        // Update entity properties - use EN translation for Name field
        existing.Name = enName ?? updateDto.Name;
        existing.StatusPosition = updateDto.StatusPosition;
        existing.ResponsibleManagerId = updateDto.ResponsibleManagerId;
        existing.NumberOfDays = updateDto.NumberOfDays;
        existing.SendToEmail = updateDto.SendToEmail;
        existing.SendNotification = updateDto.SendNotification;
        existing.IsActive = updateDto.IsActive;
        existing.UpdatedAt = DateTime.UtcNow;

        var updated = await _repository.UpdateAsync(existing);
        return MapToDto(updated, null);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _repository.DeleteAsync(id);
    }

    private SalesFunnelStatusDto MapToDto(SalesFunnelStatus status, string? languageCode = null)
    {
        var translations = status.Translations.ToDictionary(
            t => t.LanguageCode,
            t => t.Name
        );

        // Get name based on requested language, fallback to EN, then entity Name, then first available
        string displayName;
        if (!string.IsNullOrWhiteSpace(languageCode) && translations.ContainsKey(languageCode.ToUpper()))
        {
            displayName = translations[languageCode.ToUpper()];
        }
        else
        {
            displayName = translations.GetValueOrDefault("EN") 
                ?? translations.Values.FirstOrDefault() 
                ?? status.Name
                ?? string.Empty;
        }

        // Get responsible manager name if exists
        string? managerName = null;
        if (status.ResponsibleManagerId.HasValue && status.ResponsibleManager != null)
        {
            managerName = status.ResponsibleManager.FullName;
        }

        return new SalesFunnelStatusDto
        {
            Id = status.Id,
            Name = displayName,
            StatusPosition = status.StatusPosition,
            ResponsibleManagerId = status.ResponsibleManagerId,
            ResponsibleManagerName = managerName,
            NumberOfDays = status.NumberOfDays,
            SendToEmail = status.SendToEmail,
            SendNotification = status.SendNotification,
            IsActive = status.IsActive,
            Translations = translations,
            CreatedAt = status.CreatedAt,
            UpdatedAt = status.UpdatedAt
        };
    }
}

