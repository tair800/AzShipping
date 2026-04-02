namespace Request.Application.DTOs.RequestComment;

public record RequestCommentDto(Guid Id, Guid RequestId, string? Comments, DateTime Date);
