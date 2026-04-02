using Request.Application.DTOs.RequestComment;
using Request.Domain.AggregatesModel.RequestCommentAggregate;

namespace Request.Application.Features.RequestComments;

public static class RequestCommentMapper
{
    public static RequestCommentDto MapToDto(RequestComment? entity)
    {
        if (entity == null) return null!;
        return new RequestCommentDto(entity.Id, entity.RequestId, entity.Comments, entity.Date);
    }
}
