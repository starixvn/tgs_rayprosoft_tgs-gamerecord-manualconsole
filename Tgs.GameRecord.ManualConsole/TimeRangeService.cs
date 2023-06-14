namespace Tgs.GameRecord.ManualConsole;

internal class TimeRangeService : ITimeRangeService
{
    private readonly TimeZoneInfo _timeZoneInfo;

    public TimeRangeService()
    {
        _timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
    }

    public (DateTimeOffset StartTime, DateTimeOffset EndTime) Get(DateModes dateMode)
    {
        DateTime tempDateTime;
        DateTimeOffset startTime, endTime;
        if (dateMode == DateModes.Custom)
        {
            Console.Write("起始日期(yyyy-MM-dd HH:MM:SS):");
            tempDateTime = Convert.ToDateTime(Console.ReadLine() ?? throw new NullReferenceException());
            startTime = new DateTimeOffset(tempDateTime, _timeZoneInfo.GetUtcOffset(tempDateTime));
            Console.Write("結束日期(yyyy-MM-dd HH:MM:SS):");
            tempDateTime = Convert.ToDateTime(Console.ReadLine() ?? throw new NullReferenceException());
            endTime = new DateTimeOffset(tempDateTime, _timeZoneInfo.GetUtcOffset(tempDateTime));
        }
        else
        {
            Console.Write("日期(yyyy-MM-dd):");
            tempDateTime = Convert.ToDateTime(Console.ReadLine() ?? throw new NullReferenceException());
            startTime = new DateTimeOffset(tempDateTime, _timeZoneInfo.GetUtcOffset(tempDateTime));
            endTime = startTime.AddDays(1);
        }

        Console.WriteLine($"Start Time({startTime:O}) ~ End Time({endTime:O})");
        Console.WriteLine();

        return (startTime, endTime);
    }
}
