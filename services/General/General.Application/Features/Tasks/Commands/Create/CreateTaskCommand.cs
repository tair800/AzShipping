using General.Application.DTOs.Task;
using MediatR;

namespace General.Application.Features.Tasks.Commands.Create;

public record CreateTaskCommand(CreateTaskDto Dto) : IRequest<TaskDto>;
