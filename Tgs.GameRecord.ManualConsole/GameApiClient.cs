using System.Net;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Tgs.GameRecord.ManualConsole;

internal class GameApiClient : IGameApiClient
{
    private readonly HttpClient _httpClient;

    public GameApiClient(HttpClient httpClient)
    {
        this._httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async ValueTask<HttpStatusCode> PostAsync(
        Uri serviceUrl,
        string pathUrl,
        JsonObject source,
        CancellationToken cancellationToken = default)
    {
        var content = new StringContent(JsonSerializer.Serialize(source), Encoding.UTF8, MediaTypeNames.Application.Json);
        var httpResponseMessage = await _httpClient
            .PostAsync(
                requestUri: new Uri(serviceUrl, pathUrl),
                content: content,
                cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return httpResponseMessage.StatusCode;
    }
}
