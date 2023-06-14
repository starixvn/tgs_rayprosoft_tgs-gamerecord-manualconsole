using System.Text;

using Tgs.GameRecord.ManualConsole.Operators;

namespace Tgs.GameRecord.ManualConsole;

public class GameProvidersService : IGameProvidersService
{
    public GameProvidersOperator Get(IEnumerable<GameProvidersOperator> gameProviders)
    {
        var options = new StringBuilder();
        var index = 1;
        foreach (var gameProvider in gameProviders)
        {
            _ = options.AppendLine($"({index}).{gameProvider.Name}({gameProvider.Code})");
            index++;
        }
        Console.WriteLine(options);

        Console.Write("選擇遊戲商:");

        var optionIndex = Convert.ToInt32(Console.ReadLine() ?? throw new NullReferenceException());

        var optionGameProvider = gameProviders.ElementAt(optionIndex - 1);

        Console.WriteLine("Game Provider:{0}", optionGameProvider);
        Console.WriteLine();
        return optionGameProvider;
    }
}
