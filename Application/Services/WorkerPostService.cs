using AzShipping.Application.DTOs;
using AzShipping.Domain.Entities;
using AzShipping.Domain.Interfaces;

namespace AzShipping.Application.Services;

public class WorkerPostService : IWorkerPostService
{
    private readonly IWorkerPostRepository _repository;

    public WorkerPostService(IWorkerPostRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<WorkerPostDto>> GetAllAsync(string? languageCode = null)
    {
        var posts = await _repository.GetAllAsync();
        return posts.Select(p => MapToDto(p, languageCode));
    }

    public async Task<WorkerPostDto?> GetByIdAsync(Guid id, string? languageCode = null)
    {
        var post = await _repository.GetByIdAsync(id);
        return post != null ? MapToDto(post, languageCode) : null;
    }

    public async Task<WorkerPostDto> CreateAsync(CreateWorkerPostDto createDto)
    {
        var post = new WorkerPost
        {
            Id = Guid.NewGuid(),
            IsActive = createDto.IsActive,
            CreatedAt = DateTime.UtcNow
        };

        // Add translations
        var translations = new List<WorkerPostTranslation>();
        
        // Use Name as EN translation, or if EN is in translations, use that
        var enName = createDto.Translations.ContainsKey("EN") 
            ? createDto.Translations["EN"] 
            : createDto.Name;
        
        if (!string.IsNullOrWhiteSpace(enName))
        {
            translations.Add(new WorkerPostTranslation
            {
                Id = Guid.NewGuid(),
                WorkerPostId = post.Id,
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
                translations.Add(new WorkerPostTranslation
                {
                    Id = Guid.NewGuid(),
                    WorkerPostId = post.Id,
                    LanguageCode = translation.Key.ToUpper(),
                    Name = translation.Value
                });
            }
        }

        post.Translations = translations;
        var created = await _repository.CreateAsync(post);
        return MapToDto(created, null);
    }

    public async Task<WorkerPostDto?> UpdateAsync(Guid id, UpdateWorkerPostDto updateDto)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing == null)
            return null;

        // Create new translations list instead of clearing and modifying the existing collection
        var newTranslations = new List<WorkerPostTranslation>();
        
        // Use Name as EN translation, or if EN is in translations, use that
        var enName = updateDto.Translations.ContainsKey("EN") 
            ? updateDto.Translations["EN"] 
            : (updateDto.Translations.ContainsKey("en") 
                ? updateDto.Translations["en"] 
                : updateDto.Name);
        
        if (!string.IsNullOrWhiteSpace(enName))
        {
            newTranslations.Add(new WorkerPostTranslation
            {
                Id = Guid.NewGuid(),
                WorkerPostId = existing.Id,
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
                newTranslations.Add(new WorkerPostTranslation
                {
                    Id = Guid.NewGuid(),
                    WorkerPostId = existing.Id,
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

    private static WorkerPostDto MapToDto(WorkerPost post, string? languageCode = null)
    {
        var translations = post.Translations.ToDictionary(
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

        return new WorkerPostDto
        {
            Id = post.Id,
            Name = displayName,
            IsActive = post.IsActive,
            Translations = translations,
            CreatedAt = post.CreatedAt,
            UpdatedAt = post.UpdatedAt
        };
    }
}

