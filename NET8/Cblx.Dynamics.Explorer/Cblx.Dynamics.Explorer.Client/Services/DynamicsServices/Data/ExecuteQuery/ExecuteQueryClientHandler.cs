using Cblx.Dynamics.Explorer.Client;
using System.Net.Http.Json;
using System.Text.Json.Nodes;

namespace Cblx.Dynamics.Explorer.Services.DynamicsServices.ExecuteQuery;

internal class ExecuteQueryClientHandler(HttpClient client) : IExecuteQueryHandler
{
    public async Task<JsonObject?> GetAsync(string query)
        => await client.GetFromJsonAsync<JsonObject>($"{Route.GetEndpoint<IExecuteQueryHandler>()}?{nameof(query)}={query}");
}
