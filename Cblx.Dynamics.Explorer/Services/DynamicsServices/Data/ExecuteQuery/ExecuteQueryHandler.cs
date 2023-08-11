using System.Net.Http.Json;
using System.Text.Json.Nodes;

namespace Cblx.Dynamics.Explorer.Services.DynamicsServices.ExecuteQuery;

public class ExecuteQueryHandler(ExplorerHttpClient client) : IExecuteQueryHandler
{
    public async Task<JsonObject?> GetAsync(string query)
    {
        var response = await client.HttpClient.GetAsync(query.RemoveLineEndingsForODataQuery());
        return await response.Content.ReadFromJsonAsync<JsonObject>();
    }
}
