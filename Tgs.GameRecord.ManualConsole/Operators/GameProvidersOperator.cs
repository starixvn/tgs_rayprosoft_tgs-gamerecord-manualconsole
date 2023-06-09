using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tgs.GameRecord.ManualConsole.Operators;

public record GameProvidersOperator
{
    public string Name { get; init; } = default!;
    public string Code { get; init; } = default!;
    public string Path { get; init; } = default!;
    public string JsonFile { get; init; } = default!;
}
