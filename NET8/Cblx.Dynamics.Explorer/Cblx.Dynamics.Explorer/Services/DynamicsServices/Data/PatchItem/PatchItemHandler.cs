using Cblx.Dynamics.Explorer.Client.Services.DynamicsServices.Data.PatchItem;
using System.Net.Http.Json;
using System.Text.Json.Nodes;

namespace Cblx.Dynamics.Explorer.Services.DynamicsServices.Data.PatchItem;

internal class PatchItemHandler(ExplorerHttpClient client) : IPatchItem
{
    public async Task ExecuteAsync(PatchItemRequest request)
    {
        var response = await client.HttpClient.PatchAsJsonAsync($"{request.EntitySetName}({request.Id})", request.Data);
        if (!response.IsSuccessStatusCode)
        {
            var responseJson = await response.Content.ReadFromJsonAsync<JsonObject>();
            throw new InvalidOperationException(responseJson?["error"]?["message"]?.ToString() ?? "Unexpected error");
        }
    }
}
