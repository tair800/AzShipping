using MediatR;
using Settings.Application.DTOs.Bank;
using Settings.Domain.AggregatesModel.BankAggregate;

namespace Settings.Application.Features.Banks.Commands.Create;

public sealed class CreateBankCommandHandler(IBankRepository repository) : IRequestHandler<CreateBankCommand, BankDto>
{
    private readonly IBankRepository _repository = repository;

    public async Task<BankDto> Handle(CreateBankCommand request, CancellationToken cancellationToken)
    {
        var entity = new Bank
        {
            Id = Guid.NewGuid(),
            Name = request.Dto.Name,
            UnofficialName = request.Dto.UnofficialName,
            Branch = request.Dto.Branch,
            Code = request.Dto.Code,
            Swift = request.Dto.Swift,
            CountryId = request.Dto.CountryId,
            CityId = request.Dto.CityId,
            Address = request.Dto.Address,
            PostCode = request.Dto.PostCode,
            CreatedAt = DateTime.UtcNow
        };
        await _repository.AddAsync(entity, cancellationToken);
        var created = await _repository.GetByIdAsync(entity.Id, cancellationToken);
        return new BankDto
        {
            Id = created!.Id, Name = created.Name, UnofficialName = created.UnofficialName, Branch = created.Branch,
            Code = created.Code, Swift = created.Swift, CountryId = created.CountryId, CountryName = created.Country?.Name,
            CityId = created.CityId, CityName = created.City?.Name,
            Address = created.Address, PostCode = created.PostCode, CreatedAt = created.CreatedAt, UpdatedAt = created.UpdatedAt
        };
    }
}
