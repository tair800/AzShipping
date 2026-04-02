using MediatR;
using Settings.Domain.AggregatesModel.BankAggregate;

namespace Settings.Application.Features.Banks.Commands.Delete;

public sealed class DeleteBankCommandHandler(IBankRepository repository) : IRequestHandler<DeleteBankCommand, bool>
{
    private readonly IBankRepository _repository = repository;

    public async Task<bool> Handle(DeleteBankCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return false;
        await _repository.DeleteAsync(request.Id, cancellationToken);
        return true;
    }
}
