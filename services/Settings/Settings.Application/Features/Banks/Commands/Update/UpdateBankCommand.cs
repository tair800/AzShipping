using MediatR;
using Settings.Application.DTOs.Bank;

namespace Settings.Application.Features.Banks.Commands.Update;

public sealed record UpdateBankCommand(Guid Id, UpdateBankDto Dto) : IRequest<BankDto?>;
