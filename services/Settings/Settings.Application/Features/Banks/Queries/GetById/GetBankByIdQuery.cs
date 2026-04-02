using MediatR;
using Settings.Application.DTOs.Bank;

namespace Settings.Application.Features.Banks.Queries.GetById;

public sealed record GetBankByIdQuery(Guid Id) : IRequest<BankDto?>;
