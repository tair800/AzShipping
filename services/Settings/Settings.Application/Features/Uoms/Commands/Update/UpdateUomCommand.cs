using MediatR;
using Settings.Application.DTOs.Uom;

namespace Settings.Application.Features.Uoms.Commands.Update;

public sealed record UpdateUomCommand(Guid Id, UpdateUomDto Dto) : IRequest<UomDto?>;
