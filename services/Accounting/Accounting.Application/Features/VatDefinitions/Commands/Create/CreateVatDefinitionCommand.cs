using Accounting.Application.DTOs.VatDefinition;
using MediatR;

namespace Accounting.Application.Features.VatDefinitions.Commands.Create;

public record CreateVatDefinitionCommand(CreateVatDefinitionDto Dto) : IRequest<VatDefinitionDto>;
