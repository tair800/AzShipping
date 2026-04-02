using MediatR;
using Request.Application.DTOs.Request;

namespace Request.Application.Features.Requests.Commands.CreateRequestType;

public sealed record CreateRequestTypeCommand(CreateRequestTypeDto Dto) : IRequest<RequestTypeDto>;
