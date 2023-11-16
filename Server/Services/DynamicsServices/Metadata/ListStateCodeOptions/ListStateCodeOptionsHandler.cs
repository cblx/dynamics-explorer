using Cblx.Dynamics.Explorer.Models;
using System.Net.Http.Json;
using System.Text.Json.Nodes;

namespace Cblx.Dynamics.Explorer.Services.DynamicsServices.Metadata.ListStateCodeOptions;

internal class ListStateCodeOptionsHandler(ExplorerHttpClient client) : IListStateCodeOptionsHandler
{
    public async Task<PicklistOption[]> GetAsync(string entityLogicalName)
    {
        var json = await client.HttpClient.GetFromJsonAsync<JsonObject>($"EntityDefinitions(LogicalName='{entityLogicalName}')/Attributes(LogicalName='statecode')/Microsoft.Dynamics.CRM.StateAttributeMetadata?$select=LogicalName&$expand=OptionSet($select=Options)");
        return json.ToPicklistOptions();
    }
}