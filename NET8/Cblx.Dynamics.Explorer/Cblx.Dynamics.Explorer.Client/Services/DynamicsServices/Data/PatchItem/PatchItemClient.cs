
using System.Net.Http.Json;
using System.Text.Json.Nodes;

namespace Cblx.Dynamics.Explorer.Client.Services.DynamicsServices.Data.PatchItem;

public class PatchItemClient(HttpClient client) : IPatchItem
{
    public async Task ExecuteAsync(PatchItemRequest request)
    {
        var response = await client.PatchAsJsonAsync(Route.GetEndpoint<IPatchItem>(), request);
        if (!response.IsSuccessStatusCode)
        {
            var responseJson = await response.Content.ReadFromJsonAsync<JsonObject>();
            throw new InvalidOperationException(responseJson?["error"]?.ToString() ?? "Unexpected error");
        }
    }
}