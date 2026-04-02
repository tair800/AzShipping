using MediatR;
using Settings.Application.DTOs.Bank;
using Settings.Domain.AggregatesModel.BankAggregate;

namespace Settings.Application.Features.Banks.Queries.GetAll;

public sealed class GetAllBanksQueryHandler(IBankRepository repository) : IRequestHandler<GetAllBanksQuery, IReadOnlyList<BankDto>>
{
    private readonly IBankRepository _repository = repository;

    public async Task<IReadOnlyList<BankDto>> Handle(GetAllBanksQuery request, CancellationToken cancellationToken)
    {
        var list = await _repository.GetAllAsync(cancellationToken);
        return list.Select(b => new BankDto
        {
            Id = b.Id, Name = b.Name, UnofficialName = b.UnofficialName, Branch = b.Branch,
            Code = b.Code, Swift = b.Swift, CountryId = b.CountryId, CountryName = b.Country?.Name,
            CityId = b.CityId, CityName = b.City?.Name,
            Address = b.Address, PostCode = b.PostCode, CreatedAt = b.CreatedAt, UpdatedAt = b.UpdatedAt
        }).ToList();
    }
}
