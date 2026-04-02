using MediatR;
using Settings.Application.DTOs.Uom;

namespace Settings.Application.Features.Uoms.Commands.Create;

public sealed record CreateUomCommand(CreateUomDto Dto) : IRequest<UomDto>;
