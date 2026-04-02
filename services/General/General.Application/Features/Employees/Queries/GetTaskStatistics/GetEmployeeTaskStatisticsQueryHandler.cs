using System.Globalization;
using General.Application.DTOs.Employee;
using General.Domain.AggregatesModel.EmployeeAggregate;
using General.Domain.AggregatesModel.TaskAggregate;
using MediatR;

namespace General.Application.Features.Employees.Queries.GetTaskStatistics;

public class GetEmployeeTaskStatisticsQueryHandler(IEmployeeRepository employeeRepository, ITaskRepository taskRepository)
    : IRequestHandler<GetEmployeeTaskStatisticsQuery, EmployeeTaskStatisticsDto?>
{
    public async Task<EmployeeTaskStatisticsDto?> Handle(GetEmployeeTaskStatisticsQuery request, CancellationToken cancellationToken)
    {
        var employee = await employeeRepository.GetByIdAsync(request.EmployeeId, cancellationToken);
        if (employee == null) return null;

        var weekStart = request.WeekStartUtc.HasValue
            ? DateTime.SpecifyKind(request.WeekStartUtc.Value.Date, DateTimeKind.Utc)
            : WeekUtc.StartOfWeekMondayUtc(DateTime.UtcNow);

        if (weekStart.DayOfWeek != DayOfWeek.Monday)
            weekStart = WeekUtc.StartOfWeekMondayUtc(weekStart);

        var weekEndExclusive = weekStart.AddDays(7);
        var tasks = await taskRepository.GetByResponsibleUserIdCreatedInRangeAsync(
            employee.UserId, weekStart, weekEndExclusive, cancellationToken);

        var completedSet = new HashSet<Guid>(request.CompletedStatusIds);
        var total = tasks.Count;
        var completed = completedSet.Count == 0
            ? 0
            : tasks.Count(t => t.StatusId.HasValue && completedSet.Contains(t.StatusId.Value));

        var byDay = new List<DailyTaskStatDto>(7);
        for (var i = 0; i < 7; i++)
        {
            var dayStart = weekStart.AddDays(i);
            var dayEnd = dayStart.AddDays(1);
            var dateOnly = DateOnly.FromDateTime(dayStart);
            var dayTasks = tasks.Where(t => t.DateOfCreation >= dayStart && t.DateOfCreation < dayEnd).ToList();
            var dayTotal = dayTasks.Count;
            var dayCompleted = completedSet.Count == 0
                ? 0
                : dayTasks.Count(t => t.StatusId.HasValue && completedSet.Contains(t.StatusId.Value));
            byDay.Add(new DailyTaskStatDto
            {
                DayName = dayStart.ToString("dddd", CultureInfo.InvariantCulture),
                Date = dateOnly,
                Total = dayTotal,
                Completed = dayCompleted
            });
        }

        var rate = total == 0 ? 0 : Math.Round(100.0 * completed / total, 2);

        return new EmployeeTaskStatisticsDto
        {
            EmployeeId = employee.Id,
            UserId = employee.UserId,
            WeekStartUtc = weekStart,
            WeekEndUtcExclusive = weekEndExclusive,
            TotalTasks = total,
            CompletedTasks = completed,
            CompletionRatePercent = rate,
            ByDay = byDay
        };
    }
}
