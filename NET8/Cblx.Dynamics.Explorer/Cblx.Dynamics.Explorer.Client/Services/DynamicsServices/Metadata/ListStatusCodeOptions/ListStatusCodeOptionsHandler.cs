using Cblx.Dynamics.Explorer.Client;
using Cblx.Dynamics.Explorer.Models;
using Cblx.Dynamics.Explorer.Services.DynamicsServices.Metadata.ListStatusCodeOptions;
using System.Net.Http.Json;
using System.Text.Json.Nodes;

namespace Cblx.Dynamics.Explorer.Client.Services.DynamicsServices.Metadata.ListStatusCodeOptions;

internal class ListStatusCodeOptionsClientHandler(HttpClient client) : IListStatusCodeOptionsHandler
{
    public async Task<PicklistOption[]> GetAsync(string entityLogicalName)
        => (await client.GetFromJsonAsync<PicklistOption[]>($"{Route.GetEndpoint<IListStatusCodeOptionsHandler>()}?{nameof(entityLogicalName)}={entityLogicalName}"))!;

}
