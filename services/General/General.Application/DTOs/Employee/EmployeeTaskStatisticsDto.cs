namespace General.Application.DTOs.Employee;

public record EmployeeTaskStatisticsDto
{
    public Guid EmployeeId { get; init; }
    public long UserId { get; init; }
    public DateTime WeekStartUtc { get; init; }
    public DateTime WeekEndUtcExclusive { get; init; }
    public int TotalTasks { get; init; }
    public int CompletedTasks { get; init; }
    public double CompletionRatePercent { get; init; }
    public IReadOnlyList<DailyTaskStatDto> ByDay { get; init; } = [];
}

public record DailyTaskStatDto
{
    public string DayName { get; init; } = "";
    public DateOnly Date { get; init; }
    public int Total { get; init; }
    public int Completed { get; init; }
}
