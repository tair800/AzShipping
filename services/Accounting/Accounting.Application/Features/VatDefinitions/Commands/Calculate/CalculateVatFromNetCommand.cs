using Accounting.Application.DTOs.VatDefinition;
using MediatR;

namespace Accounting.Application.Features.VatDefinitions.Commands.Calculate;

public record CalculateVatFromNetCommand(CalculateVatFromNetRequestDto Request) : IRequest<CalculateVatFromNetResultDto?>;
