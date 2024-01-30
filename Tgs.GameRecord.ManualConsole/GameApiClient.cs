using System.Net;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Tgs.GameRecord.ManualConsole;

internal class GameApiClient(HttpClient httpClient) : IGameApiClient
{
    private static readonly JsonSerializerOptions _SerializerOptions = new()
    {
        WriteIndented = false,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Converters = { new JsonStringEnumConverter() }
    };

    public async ValueTask<HttpStatusCode> PostAsync(Uri serviceUrl, string pathUrl, JsonObject source, CancellationToken cancellationToken = default)
    {
        var content = JsonContent.Create(source, options: _SerializerOptions);
        var httpResponseMessage = await httpClient.PostAsync(
            requestUri: new Uri(serviceUrl, pathUrl),
            content: content,
            cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return httpResponseMessage.StatusCode;
    }
}
