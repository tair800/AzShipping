using MediatR;
using Settings.Application.DTOs.Bank;

namespace Settings.Application.Features.Banks.Commands.Create;

public sealed record CreateBankCommand(CreateBankDto Dto) : IRequest<BankDto>;
