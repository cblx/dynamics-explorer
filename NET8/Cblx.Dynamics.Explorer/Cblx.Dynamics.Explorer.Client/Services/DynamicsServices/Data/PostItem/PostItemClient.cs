using System.Net.Http.Json;

namespace Cblx.Dynamics.Explorer.Client.Services.DynamicsServices.Data.PostItem;

public class PostItemClient(HttpClient client) : IPostItem
{
    public async Task ExecuteAsync(PostItemRequest request)
        => await client.PostAsJsonAsync(Route.GetEndpoint<IPostItem>(), request);
}