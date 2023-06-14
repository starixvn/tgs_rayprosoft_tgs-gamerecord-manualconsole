using Tgs.GameRecord.ManualConsole.Operators;

namespace Tgs.GameRecord.ManualConsole;

public interface ITimeIntervalsService
{
    public TimeIntervalsOperator Get(IEnumerable<TimeIntervalsOperator> timeIntervals);
}
