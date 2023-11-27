using Cblx.Dynamics.Explorer.Services.DynamicsServices.ExecuteQuery;
using System.Net.Http.Json;
using System.Text.Json.Nodes;

namespace Cblx.Dynamics.Explorer.Services.DynamicsServices.Data.ExecuteQuery;

public class ExecuteQueryHandler(ExplorerHttpClient client, UserContext userContext) : IExecuteQueryHandler
{
    public async Task<JsonObject?> GetAsync(string query)
    {
        userContext.AssertCanReadCurrentInstance();
        var response = await client.HttpClient.GetAsync(query.RemoveLineEndingsForODataQuery());
        var jsonObject = await response.Content.ReadFromJsonAsync<JsonObject>();
        return jsonObject;
    }
}
