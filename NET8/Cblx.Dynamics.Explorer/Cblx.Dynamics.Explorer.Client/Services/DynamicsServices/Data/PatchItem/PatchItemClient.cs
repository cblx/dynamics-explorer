
using System.Net.Http.Json;

namespace Cblx.Dynamics.Explorer.Client.Services.DynamicsServices.Data.PatchItem;

public class PatchItemClient(HttpClient client) : IPatchItem
{
    public async Task ExecuteAsync(PatchItemRequest request)
        => await client.PatchAsJsonAsync(Route.GetEndpoint<IPatchItem>(), request);
}