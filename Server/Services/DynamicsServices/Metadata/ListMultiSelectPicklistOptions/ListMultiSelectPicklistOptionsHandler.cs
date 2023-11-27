using Cblx.Dynamics.Explorer.Models;
using System.Text.Json.Nodes;
using System.Net.Http.Json;
using Cblx.Dynamics.Explorer.Services.DynamicsServices.ListMultiSelectPicklistOptions;

namespace Cblx.Dynamics.Explorer.Services.DynamicsServices.Metadata.ListMultiSelectPicklistOptions;

internal class ListMultiSelectPicklistOptionsHandler(ExplorerHttpClient client, UserContext userContext) : IListMultiSelectPicklistOptionsHandler
{
    public async Task<PicklistOption[]> GetAsync(string entityLogicalName, string attributeLogicalName)
    {
        userContext.AssertCanReadCurrentInstance();
        var json = await client.HttpClient.GetFromJsonAsync<JsonObject>($"EntityDefinitions(LogicalName='{entityLogicalName}')/Attributes(LogicalName='{attributeLogicalName}')/Microsoft.Dynamics.CRM.MultiSelectPicklistAttributeMetadata?$select=LogicalName&$expand=OptionSet($select=Options)");
        return json.ToPicklistOptions();
    }
}
