using General.Application.DTOs.Currency;
using MediatR;

namespace General.Application.Features.Currencies.Queries.GetAll;

public record GetAllCurrenciesQuery : IRequest<IReadOnlyList<CurrencyDto>>;
