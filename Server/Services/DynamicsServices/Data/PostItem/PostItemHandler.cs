using Cblx.Dynamics.Explorer.Client.Services.DynamicsServices.Data.PostItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Cblx.Dynamics.Explorer.Services.DynamicsServices.Data.PostItem;

internal class PostItemHandler(ExplorerHttpClient client) : IPostItem
{
    public async Task ExecuteAsync(PostItemRequest request)
    {
        var response = await client.HttpClient.PostAsJsonAsync(request.EntitySetName, request.Data);
        if (!response.IsSuccessStatusCode)
        {
            var responseJson = await response.Content.ReadFromJsonAsync<JsonObject>();
            throw new InvalidOperationException(responseJson?["error"]?["message"]?.ToString() ?? "Unexpected error");
        }
    }
}
