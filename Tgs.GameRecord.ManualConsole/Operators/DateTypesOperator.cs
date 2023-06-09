using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tgs.GameRecord.ManualConsole.Operators;

public record DateTypesOperator
{
    public string Name { get; init; } = default!;
    public int Seconds { get; init; } = default!;
}
