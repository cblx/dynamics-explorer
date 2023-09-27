using Cblx.Dynamics.Explorer.Models;
using Cblx.Dynamics.Explorer.Services.DynamicsServices.Metadata.ListStateCodeOptions;
using System.Net.Http.Json;

namespace Cblx.Dynamics.Explorer.Client.Services.DynamicsServices.Metadata.ListStateCodeOptions;

internal class ListStateCodeOptionsClientHandler(HttpClient client) : IListStateCodeOptionsHandler
{
    public async Task<PicklistOption[]> GetAsync(string entityLogicalName)
        => (await client.GetFromJsonAsync<PicklistOption[]>($"{Route.GetEndpoint<IListStateCodeOptionsHandler>()}?entityLogicalName={entityLogicalName}"))!;
}