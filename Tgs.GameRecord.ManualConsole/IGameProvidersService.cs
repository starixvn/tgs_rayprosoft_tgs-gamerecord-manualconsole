using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tgs.GameRecord.ManualConsole.Operators;

namespace Tgs.GameRecord.ManualConsole;

public interface IGameProvidersService
{
    public GameProvidersOperator Get(IEnumerable<GameProvidersOperator> gameProviders);
}
