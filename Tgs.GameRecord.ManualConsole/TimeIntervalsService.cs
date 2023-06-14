using System.Text;
using Tgs.GameRecord.ManualConsole.Operators;

namespace Tgs.GameRecord.ManualConsole;

internal class TimeIntervalsService : ITimeIntervalsService
{
    public TimeIntervalsOperator Get(IEnumerable<TimeIntervalsOperator> timeIntervals)
    {
        var options = new StringBuilder();
        var index = 1;
        foreach (var timeInterval in timeIntervals)
        {
            _ = options.AppendLine($"({index}).{timeInterval.Name}");
            index++;
        }
        Console.WriteLine(options);

        Console.Write("選擇時間間隔:");

        var optionIndex = Convert.ToInt32(Console.ReadLine() ?? throw new NullReferenceException());

        var optionTimeInterval = timeIntervals.ElementAt(optionIndex - 1);
        Console.WriteLine("Time Interval:{0}", optionTimeInterval);
        Console.WriteLine();
        return optionTimeInterval;
    }
}
