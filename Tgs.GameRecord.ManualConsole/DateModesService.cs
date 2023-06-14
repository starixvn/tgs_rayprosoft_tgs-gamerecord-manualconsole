using System.Text;

namespace Tgs.GameRecord.ManualConsole;

internal class DateModesService : IDateModesService
{
    public DateModes Get()
    {
        var dict = Enum.GetValues(typeof(DateModes))
               .Cast<DateModes>()
               .ToDictionary(t => (int)t, t => t.Description());

        var options = new StringBuilder();
        foreach (var dateType in dict)
        {
            _ = options.AppendLine($"({dateType.Key + 1}).{dateType.Value}");
        }
        Console.WriteLine(options);

        Console.Write("選擇日期模式:");

        var optionDateType = (DateModes)Convert.ToInt32(Console.ReadLine() ?? throw new NullReferenceException()) - 1;

        Console.WriteLine("Date Mode:{0}", optionDateType.Description());
        Console.WriteLine();
        return optionDateType;
    }
}
