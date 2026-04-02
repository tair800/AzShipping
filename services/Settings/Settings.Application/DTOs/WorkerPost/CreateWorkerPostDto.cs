namespace Settings.Application.DTOs.WorkerPost;

public record CreateWorkerPostDto(string Name, bool IsActive, Dictionary<string, string>? Translations = null);
