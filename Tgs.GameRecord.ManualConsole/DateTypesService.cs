using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            options.AppendLine($"({index}).{dateType.Name}");
            index++;
        }
        Console.WriteLine(options);

        Console.Write("選擇時間類型:");

        var optionIndex = Convert.ToInt32(Console.ReadLine() ?? throw new NullReferenceException());

        return dateTypes.ElementAt(optionIndex - 1);
    }
}
