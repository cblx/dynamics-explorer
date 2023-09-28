using System.Text.Json.Nodes;

namespace Cblx.Dynamics.Explorer.Client.Services.DynamicsServices.Data.ListItems;

internal interface IListItems
{
    Task<JsonObject[]> HandleAsync(ListItemsRequest request);
}
