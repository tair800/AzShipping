using MediatR;
using Settings.Application.DTOs.Numeration;

namespace Settings.Application.Features.Numerations.Queries.Preview;

public sealed record PreviewNumerationQuery(NumerationGenerateRequestDto Dto) : IRequest<NumerationGenerateResponseDto>;
