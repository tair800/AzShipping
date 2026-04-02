using Carrier.Domain.AggregatesModel.DriverAggregate;
using MediatR;

namespace Carrier.Application.Features.Drivers.Commands.UpdateDocumentPath;

public class UpdateDriverDocumentPathCommandHandler(IDriverRepository repository) : IRequestHandler<UpdateDriverDocumentPathCommand, bool>
{
    public async Task<bool> Handle(UpdateDriverDocumentPathCommand request, CancellationToken cancellationToken)
    {
        var driver = await repository.GetByIdAsync(request.DriverId, cancellationToken);
        if (driver == null) return false;
        if (string.Equals(request.DocumentType, "passport", StringComparison.OrdinalIgnoreCase))
            driver.PassportFilePath = request.RelativeFilePath;
        else if (string.Equals(request.DocumentType, "drivinglicence", StringComparison.OrdinalIgnoreCase))
            driver.DrivingLicenceFilePath = request.RelativeFilePath;
        else return false;
        driver.UpdatedAt = DateTime.UtcNow;
        await repository.UpdateAsync(driver, cancellationToken);
        return true;
    }
}
