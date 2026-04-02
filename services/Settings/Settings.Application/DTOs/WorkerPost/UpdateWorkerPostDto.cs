namespace Settings.Application.DTOs.WorkerPost;

public record UpdateWorkerPostDto(string Name, bool IsActive, Dictionary<string, string>? Translations = null);
