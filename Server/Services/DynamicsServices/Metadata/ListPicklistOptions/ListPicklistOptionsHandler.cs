using Cblx.Dynamics.Explorer.Models;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Nodes;

namespace Cblx.Dynamics.Explorer.Services.DynamicsServices.Metadata.ListPicklistOptions;

internal class ListPicklistOptionsHandler(ExplorerHttpClient client, UserContext userContext) : IListPicklistOptionsHandler
{
    public async Task<PicklistOption[]> GetAsync(string entityLogicalName, string attributeLogicalName)
    {
        userContext.AssertCanReadCurrentInstance();
        var json = await client.HttpClient.GetFromJsonAsync<JsonObject>($"EntityDefinitions(LogicalName='{entityLogicalName}')/Attributes(LogicalName='{attributeLogicalName}')/Microsoft.Dynamics.CRM.PicklistAttributeMetadata?$select=LogicalName&$expand=OptionSet($select=Options)");
        return json.ToPicklistOptions();
    }
}
