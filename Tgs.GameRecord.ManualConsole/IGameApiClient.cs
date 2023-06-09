using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Tgs.GameRecord.ManualConsole;

public interface IGameApiClient
{
    public ValueTask<HttpStatusCode> PostAsync(
        Uri serviceUrl,
        string pathUrl,
        JsonObject source,
        CancellationToken cancellationToken = default);
}
