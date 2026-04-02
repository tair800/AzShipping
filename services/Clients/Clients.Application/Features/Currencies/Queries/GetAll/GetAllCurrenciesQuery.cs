using Clients.Application.DTOs.Currency;
using MediatR;

namespace Clients.Application.Features.Currencies.Queries.GetAll;

public sealed record GetAllCurrenciesQuery : IRequest<IReadOnlyList<CurrencyDto>>;
