using MediatR;

namespace Settings.Application.Features.Departments.Commands.Delete;

public sealed record DeleteDepartmentCommand(Guid Id) : IRequest<bool>;
