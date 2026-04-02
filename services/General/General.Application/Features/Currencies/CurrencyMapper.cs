using General.Application.DTOs.Currency;
using CurrencyEntity = General.Domain.AggregatesModel.CurrencyAggregate.Currency;

namespace General.Application.Features.Currencies;

public static class CurrencyMapper
{
    public static CurrencyDto MapToDto(CurrencyEntity entity) => new()
    {
        Id = entity.Id,
        Code = entity.Code,
        Name = entity.Name,
        Symbol = entity.Symbol,
        NumericCode = entity.NumericCode
    };
}
