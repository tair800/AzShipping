namespace General.Application.Features.Employees;

internal static class WeekUtc
{
    /// <summary>Monday 00:00 UTC of the week containing <paramref name="referenceUtc"/>.</summary>
    public static DateTime StartOfWeekMondayUtc(DateTime referenceUtc)
    {
        var dt = referenceUtc.Kind == DateTimeKind.Utc ? referenceUtc : referenceUtc.ToUniversalTime();
        var date = dt.Date;
        var day = (int)date.DayOfWeek;
        var mondayOffset = day == 0 ? -6 : 1 - day;
        return DateTime.SpecifyKind(date.AddDays(mondayOffset), DateTimeKind.Utc);
    }
}
