using MediatR;
using Settings.Application.DTOs.Bank;

namespace Settings.Application.Features.Banks.Queries.GetAll;

public sealed record GetAllBanksQuery : IRequest<IReadOnlyList<BankDto>>;
