using MediatR;

namespace General.Application.Features.Vessels.Commands.Delete;

public record DeleteVesselCommand(Guid Id, bool SoftDelete = false) : IRequest<bool>;
