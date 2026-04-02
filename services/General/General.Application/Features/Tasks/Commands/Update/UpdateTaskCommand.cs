using General.Application.DTOs.Task;
using MediatR;

namespace General.Application.Features.Tasks.Commands.Update;

public record UpdateTaskCommand(Guid Id, UpdateTaskDto Dto) : IRequest<TaskDto?>;
