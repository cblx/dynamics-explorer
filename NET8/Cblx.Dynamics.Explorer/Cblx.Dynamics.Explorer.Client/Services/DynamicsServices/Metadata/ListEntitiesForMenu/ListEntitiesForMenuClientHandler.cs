using Cblx.Dynamics.Explorer.Services.DynamicsServices.ListTablesForMenu;
using System.Net.Http.Json;

namespace Cblx.Dynamics.Explorer.Client.Services.DynamicsServices.Metadata.ListEntitiesForMenu;

public class ListEntitiesForMenuClientHandler(HttpClient client) : IListEntitiesForMenuHandler
{
    public async Task<EntityDto[]> GetAsync()
        => (await client.GetFromJsonAsync<EntityDto[]>($"{Routes.GetEndpoint<IListEntitiesForMenuHandler>()}"))!;
}