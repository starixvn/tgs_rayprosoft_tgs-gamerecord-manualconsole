using System.Text;

using Tgs.GameRecord.ManualConsole.Operators;

namespace Tgs.GameRecord.ManualConsole;

internal class DateTypesService : IDateTypesService
{
    public DateTypesOperator Get(IEnumerable<DateTypesOperator> dateTypes)
    {
        var options = new StringBuilder();
        var index = 1;
        foreach (var dateType in dateTypes)
        {
            _ = options.AppendLine($"({index}).{dateType.Name}");
            index++;
        }
        Console.WriteLine(options);

        Console.Write("選擇時間類型:");

        var optionIndex = Convert.ToInt32(Console.ReadLine() ?? throw new NullReferenceException());

        var optionDateType = dateTypes.ElementAt(optionIndex - 1);
        Console.WriteLine("Date Type:{0}", optionDateType);
        Console.WriteLine();
        return optionDateType;
    }
}
