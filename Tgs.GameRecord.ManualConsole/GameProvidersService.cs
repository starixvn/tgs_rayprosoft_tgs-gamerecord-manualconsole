using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tgs.GameRecord.ManualConsole.Operators;

namespace Tgs.GameRecord.ManualConsole;

public class GameProvidersService: IGameProvidersService
{
    public GameProvidersOperator Get(IEnumerable<GameProvidersOperator> gameProviders)
    {
        var options = new StringBuilder();
        var index = 1;
        foreach (var gameProvider in gameProviders)
        {
            options.AppendLine($"({index}).{gameProvider.Name}({gameProvider.Code})");
            index++;
        }
        Console.WriteLine(options);

        Console.Write("選擇遊戲商:");

        var optionIndex = Convert.ToInt32(Console.ReadLine() ?? throw new NullReferenceException());

        return gameProviders.ElementAt(optionIndex - 1);
    }
}
