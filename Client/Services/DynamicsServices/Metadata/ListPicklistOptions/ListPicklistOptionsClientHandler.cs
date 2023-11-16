using Cblx.Dynamics.Explorer.Models;
using Cblx.Dynamics.Explorer.Services.DynamicsServices.Metadata.ListPicklistOptions;
using System.Net.Http.Json;

namespace Cblx.Dynamics.Explorer.Client.Services.DynamicsServices.Metadata.ListPicklistOptions;

internal class ListPicklistOptionsClientHandler(HttpClient client) : IListPicklistOptionsHandler
{
    public async Task<PicklistOption[]> GetAsync(string entityLogicalName, string attributeLogicalName)
        => (await client.GetFromJsonAsync<PicklistOption[]>($"{Routes.GetEndpoint<IListPicklistOptionsHandler>()}?{nameof(entityLogicalName)}={entityLogicalName}&{nameof(attributeLogicalName)}={attributeLogicalName}"))!;
}
