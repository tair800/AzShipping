using MediatR;
using Request.Application.Services;
using Request.Domain.AggregatesModel.RequestAggregate;

namespace Request.Application.Features.Requests.Commands.Delete;

public sealed class DeleteRequestCommandHandler(
    IRequestRepository repository,
    IRequestDimensionRepository dimensionRepository,
    IActionLogClient actionLogClient) : IRequestHandler<DeleteRequestCommand, bool>
{
    public async Task<bool> Handle(DeleteRequestCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return false;
        var reqNumber = entity.RequestNumber;
        var managerId = entity.ManagerId;
        var managerName = entity.ManagerName;
        await dimensionRepository.DeleteByRequestIdAsync(request.Id, cancellationToken);
        await repository.DeleteAsync(request.Id, cancellationToken);

        await actionLogClient.LogAsync("Request has been rejected and archived", $"request number: {reqNumber} • id: {request.Id}", managerId, managerName, cancellationToken);

        return true;
    }
}
