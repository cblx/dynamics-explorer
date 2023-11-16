using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Nodes;

namespace Cblx.Dynamics.Explorer.Client.Services.DynamicsServices.Data.ListItems;

public class ListItemsClient(HttpClient client) : IListItems
{
    public async Task<JsonObject[]> HandleAsync(ListItemsRequest request)
    {
        var sb = new StringBuilder(Routes.GetEndpoint<IListItems>())
            .Append('?')
            .Append($"{nameof(request.EntitySetName)}={request.EntitySetName}")
            .Append('&')
            .Append($"{nameof(request.Skip)}={request.Skip}")
            .Append('&')
            .Append($"{nameof(request.Take)}={request.Take}");

        return (await client.GetFromJsonAsync<JsonObject[]>(sb.ToString()))!;
    }
}