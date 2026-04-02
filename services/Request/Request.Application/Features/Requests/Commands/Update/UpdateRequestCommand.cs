using MediatR;
using Request.Application.DTOs.Request;

namespace Request.Application.Features.Requests.Commands.Update;

public sealed record UpdateRequestCommand(Guid Id, UpdateRequestDto Dto) : IRequest<RequestDto?>;
