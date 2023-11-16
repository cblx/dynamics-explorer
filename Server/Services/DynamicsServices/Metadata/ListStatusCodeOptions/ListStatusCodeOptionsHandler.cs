using Cblx.Dynamics.Explorer.Models;
using System.Net.Http.Json;
using System.Text.Json.Nodes;

namespace Cblx.Dynamics.Explorer.Services.DynamicsServices.Metadata.ListStatusCodeOptions;

internal class ListStatusCodeOptionsHandler(ExplorerHttpClient client) : IListStatusCodeOptionsHandler
{
    public async Task<PicklistOption[]> GetAsync(string entityLogicalName)
    {
        var json = await client.HttpClient.GetFromJsonAsync<JsonObject>($"EntityDefinitions(LogicalName='{entityLogicalName}')/Attributes(LogicalName='statuscode')/Microsoft.Dynamics.CRM.StatusAttributeMetadata?$select=LogicalName&$expand=OptionSet($select=Options)");
        return json.ToPicklistOptions();
    }
}
