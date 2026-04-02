using Accounting.Application.DTOs.VatDefinition;
using MediatR;

namespace Accounting.Application.Features.VatDefinitions.Queries.GetAll;

public record GetAllVatDefinitionsQuery : IRequest<IReadOnlyList<VatDefinitionDto>>;
