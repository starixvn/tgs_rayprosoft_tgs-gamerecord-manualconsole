using System.Text.Json.Nodes;

namespace Tgs.GameRecord.ManualConsole;

public interface IConfigurationService
{
    ValueTask<JsonObject> GetAsync(
       string jsonFile,
       CancellationToken cancellationToken = default);
}
