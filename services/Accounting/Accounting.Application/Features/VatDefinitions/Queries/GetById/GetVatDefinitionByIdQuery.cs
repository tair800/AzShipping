using Accounting.Application.DTOs.VatDefinition;
using MediatR;

namespace Accounting.Application.Features.VatDefinitions.Queries.GetById;

public record GetVatDefinitionByIdQuery(Guid Id) : IRequest<VatDefinitionDto?>;
