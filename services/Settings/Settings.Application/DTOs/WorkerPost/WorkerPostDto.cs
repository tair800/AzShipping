namespace Settings.Application.DTOs.WorkerPost;

public record WorkerPostDto(Guid Id, string Name, bool IsActive, Dictionary<string, string> Translations, DateTime CreatedAt, DateTime? UpdatedAt);
