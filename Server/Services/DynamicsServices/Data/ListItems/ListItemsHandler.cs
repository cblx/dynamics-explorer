using Cblx.Dynamics.Explorer.Client.Services.DynamicsServices.Data.ListItems;
using System.Text.Json.Nodes;

namespace Cblx.Dynamics.Explorer.Services.DynamicsServices.Data.ListItems;

internal class ListItemsHandler(ExplorerHttpClient client) : IListItems
{
    public Task<JsonObject[]> HandleAsync(ListItemsRequest request)
    {
        return null;
        //string query = $"{request!.EntitySetName}";
        ////if (state.SortByColumn != null)
        ////{
        ////    query += $"&$orderby={state.SortByColumn.Title} {(state.SortByAscending ? "asc" : "desc")}";
        ////}

        //var response = await client.HttpClient.GetAsync(query.RemoveLineEndingsForODataQuery());
        //var jsonObject = await response.Content.ReadFromJsonAsync<JsonObject>();
        //return jsonObject;
    }
}
