using System.Net.Http.Json;
using System.Text.Json.Nodes;

namespace Cblx.Dynamics.Explorer.Client.Services.DynamicsServices.Data.PostItem;

public class PostItemClient(HttpClient client) : IPostItem
{
    public async Task ExecuteAsync(PostItemRequest request)
    {
        var response = await client.PostAsJsonAsync(Routes.GetEndpoint<IPostItem>(), request);
        if (!response.IsSuccessStatusCode)
        {
            var responseJson = await response.Content.ReadFromJsonAsync<JsonObject>();
            throw new InvalidOperationException(responseJson?["error"]?.ToString() ?? "Unexpected error");
        }
    }
}