using MediatR;
using Request.Application.DTOs.Request;

namespace Request.Application.Features.Requests.Commands.Create;

public sealed record CreateRequestCommand(CreateRequestDto Dto) : IRequest<RequestDto>;
