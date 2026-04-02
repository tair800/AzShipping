using MediatR;
using Settings.Application.DTOs.Bank;
using Settings.Domain.AggregatesModel.BankAggregate;

namespace Settings.Application.Features.Banks.Commands.Update;

public sealed class UpdateBankCommandHandler(IBankRepository repository) : IRequestHandler<UpdateBankCommand, BankDto?>
{
    private readonly IBankRepository _repository = repository;

    public async Task<BankDto?> Handle(UpdateBankCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return null;
        entity.Name = request.Dto.Name;
        entity.UnofficialName = request.Dto.UnofficialName;
        entity.Branch = request.Dto.Branch;
        entity.Code = request.Dto.Code;
        entity.Swift = request.Dto.Swift;
        entity.CountryId = request.Dto.CountryId;
        entity.CityId = request.Dto.CityId;
        entity.Address = request.Dto.Address;
        entity.PostCode = request.Dto.PostCode;
        entity.UpdatedAt = DateTime.UtcNow;
        await _repository.UpdateAsync(entity, cancellationToken);
        var updated = await _repository.GetByIdAsync(entity.Id, cancellationToken);
        return new BankDto
        {
            Id = updated!.Id, Name = updated.Name, UnofficialName = updated.UnofficialName, Branch = updated.Branch,
            Code = updated.Code, Swift = updated.Swift, CountryId = updated.CountryId, CountryName = updated.Country?.Name,
            CityId = updated.CityId, CityName = updated.City?.Name,
            Address = updated.Address, PostCode = updated.PostCode, CreatedAt = updated.CreatedAt, UpdatedAt = updated.UpdatedAt
        };
    }
}
