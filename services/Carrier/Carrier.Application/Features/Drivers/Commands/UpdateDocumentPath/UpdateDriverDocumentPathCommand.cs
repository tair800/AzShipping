using MediatR;

namespace Carrier.Application.Features.Drivers.Commands.UpdateDocumentPath;

public record UpdateDriverDocumentPathCommand(Guid DriverId, string DocumentType, string RelativeFilePath) : IRequest<bool>;
