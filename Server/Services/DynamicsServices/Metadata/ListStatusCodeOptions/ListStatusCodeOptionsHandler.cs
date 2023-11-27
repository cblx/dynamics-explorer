using Cblx.Dynamics.Explorer.Models;
using System.Net.Http.Json;
using System.Text.Json.Nodes;

namespace Cblx.Dynamics.Explorer.Services.DynamicsServices.Metadata.ListStatusCodeOptions;

internal class ListStatusCodeOptionsHandler(ExplorerHttpClient client, UserContext userContext) : IListStatusCodeOptionsHandler
{
    public async Task<PicklistOption[]> GetAsync(string entityLogicalName)
    {
        userContext.AssertCanReadCurrentInstance();
        var json = await client.HttpClient.GetFromJsonAsync<JsonObject>($"EntityDefinitions(LogicalName='{entityLogicalName}')/Attributes(LogicalName='statuscode')/Microsoft.Dynamics.CRM.StatusAttributeMetadata?$select=LogicalName&$expand=OptionSet($select=Options)");
        return json.ToPicklistOptions();
    }
}
