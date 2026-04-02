using MediatR;
using Settings.Application.DTOs.EmailSetting;

namespace Settings.Application.Features.EmailSettings.Commands.SendSystem;

public sealed record SendSystemEmailCommand(SendSystemEmailDto Dto) : IRequest<bool>;
