namespace Request.Application.DTOs.RequestComment;

public record CreateRequestCommentDto(Guid RequestId, string? Comments);
