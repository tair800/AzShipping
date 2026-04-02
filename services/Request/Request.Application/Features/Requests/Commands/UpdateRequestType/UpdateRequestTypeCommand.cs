using MediatR;
using Request.Application.DTOs.Request;

namespace Request.Application.Features.Requests.Commands.UpdateRequestType;

public sealed record UpdateRequestTypeCommand(Guid Id, UpdateRequestTypeDto Dto) : IRequest<RequestTypeDto?>;
