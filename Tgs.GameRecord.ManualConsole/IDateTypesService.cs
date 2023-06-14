using Tgs.GameRecord.ManualConsole.Operators;

namespace Tgs.GameRecord.ManualConsole;

public interface IDateTypesService
{
    public DateTypesOperator Get(IEnumerable<DateTypesOperator> dateTypes);
}
