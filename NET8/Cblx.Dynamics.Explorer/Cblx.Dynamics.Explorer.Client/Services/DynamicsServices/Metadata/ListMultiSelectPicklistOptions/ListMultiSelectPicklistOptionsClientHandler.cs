using Cblx.Dynamics.Explorer.Models;
using System.Net.Http.Json;
using Cblx.Dynamics.Explorer.Services.DynamicsServices.ListMultiSelectPicklistOptions;

namespace Cblx.Dynamics.Explorer.Client.Services.DynamicsServices.Metadata.ListMultiSelectPicklistOptions;

internal class ListMultiSelectPicklistOptionsClientHandler(HttpClient client) : IListMultiSelectPicklistOptionsHandler
{
    public async Task<PicklistOption[]> GetAsync(string entityLogicalName, string attributeLogicalName)
    {
        return (await client.GetFromJsonAsync<PicklistOption[]>(
            $"{Route.GetEndpoint<IListMultiSelectPicklistOptionsHandler>()}?entityLogicalName={entityLogicalName}&attributeLogicalName={attributeLogicalName}"
        ))!;
    }
}
