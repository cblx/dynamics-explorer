using Cblx.Dynamics.Explorer.Client.Services.DynamicsServices.Data.PatchItem;
using System.Net.Http.Json;

namespace Cblx.Dynamics.Explorer.Services.DynamicsServices.Data.PatchItem;

internal class PatchItemHandler(ExplorerHttpClient client) : IPatchItem
{
    public Task ExecuteAsync(PatchItemRequest request)
        => client.HttpClient.PatchAsJsonAsync($"{request.EntitySetName}({request.Id})", request.Data);
}
