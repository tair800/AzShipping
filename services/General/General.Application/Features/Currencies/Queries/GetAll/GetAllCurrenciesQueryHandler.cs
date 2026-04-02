using General.Application.DTOs.Currency;
using General.Application.Features.Currencies;
using General.Domain.AggregatesModel.CurrencyAggregate;
using MediatR;

namespace General.Application.Features.Currencies.Queries.GetAll;

public class GetAllCurrenciesQueryHandler(ICurrencyRepository repository)
    : IRequestHandler<GetAllCurrenciesQuery, IReadOnlyList<CurrencyDto>>
{
    public async Task<IReadOnlyList<CurrencyDto>> Handle(GetAllCurrenciesQuery request, CancellationToken cancellationToken)
    {
        var items = await repository.GetAllAsync(cancellationToken);
        return items.Select(CurrencyMapper.MapToDto).ToList();
    }
}
