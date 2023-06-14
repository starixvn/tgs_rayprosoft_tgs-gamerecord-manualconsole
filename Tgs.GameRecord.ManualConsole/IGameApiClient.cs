using System.Net;
using System.Text.Json.Nodes;

namespace Tgs.GameRecord.ManualConsole;

public interface IGameApiClient
{
    public ValueTask<HttpStatusCode> PostAsync(
        Uri serviceUrl,
        string pathUrl,
        JsonObject source,
        CancellationToken cancellationToken = default);
}
