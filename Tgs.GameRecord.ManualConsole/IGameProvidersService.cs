using Tgs.GameRecord.ManualConsole.Operators;

namespace Tgs.GameRecord.ManualConsole;

public interface IGameProvidersService
{
    public GameProvidersOperator Get(IEnumerable<GameProvidersOperator> gameProviders);
}
