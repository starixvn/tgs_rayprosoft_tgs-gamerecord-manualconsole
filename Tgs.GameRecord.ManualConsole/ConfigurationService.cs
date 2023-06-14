using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Tgs.GameRecord.ManualConsole;

internal class ConfigurationService : IConfigurationService
{
    public async ValueTask<JsonObject> GetAsync(
        string jsonFile,
        CancellationToken cancellationToken = default)
    {
        using var reader = new StreamReader(jsonFile);
        string json = await reader.ReadToEndAsync(cancellationToken).ConfigureAwait(false);
        var jsonObjects = JsonSerializer.Deserialize<IEnumerable<JsonObject>>(json)
            ?? throw new NullReferenceException();

        var options = new StringBuilder();
        var index = 1;
        foreach (var jsonObject in jsonObjects)
        {
            _ = options.AppendLine($"({index}).{jsonObject["BrandCode"]}");
            index++;
        }

        Console.WriteLine(options);

        Console.Write("選擇BrandCode:");

        var optionIndex = Convert.ToInt32(Console.ReadLine() ?? throw new NullReferenceException());
        var optionJsonObjects = jsonObjects.ElementAt(optionIndex - 1);
        Console.WriteLine("Configuration:{0}", optionJsonObjects);
        Console.WriteLine();

        return optionJsonObjects;
    }
}
