using Accounting.Application.DTOs.VatDefinition;
using MediatR;

namespace Accounting.Application.Features.VatDefinitions.Commands.Update;

public record UpdateVatDefinitionCommand(Guid Id, UpdateVatDefinitionDto Dto) : IRequest<VatDefinitionDto?>;
