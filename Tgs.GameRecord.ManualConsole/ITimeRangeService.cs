namespace Tgs.GameRecord.ManualConsole;

public interface ITimeRangeService
{
    public (DateTimeOffset StartTime, DateTimeOffset EndTime) Get(DateModes dateMode);
}
