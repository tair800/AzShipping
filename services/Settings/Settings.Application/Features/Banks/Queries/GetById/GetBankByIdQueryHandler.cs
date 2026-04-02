using MediatR;
using Settings.Application.DTOs.Bank;
using Settings.Domain.AggregatesModel.BankAggregate;

namespace Settings.Application.Features.Banks.Queries.GetById;

public sealed class GetBankByIdQueryHandler(IBankRepository repository) : IRequestHandler<GetBankByIdQuery, BankDto?>
{
    private readonly IBankRepository _repository = repository;

    public async Task<BankDto?> Handle(GetBankByIdQuery request, CancellationToken cancellationToken)
    {
        var b = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (b == null) return null;
        return new BankDto
        {
            Id = b.Id, Name = b.Name, UnofficialName = b.UnofficialName, Branch = b.Branch,
            Code = b.Code, Swift = b.Swift, CountryId = b.CountryId, CountryName = b.Country?.Name,
            CityId = b.CityId, CityName = b.City?.Name,
            Address = b.Address, PostCode = b.PostCode, CreatedAt = b.CreatedAt, UpdatedAt = b.UpdatedAt
        };
    }
}
