using Cblx.Dynamics.Explorer.Services.DynamicsServices.ListTablesForMenu;
using System.Net.Http.Json;
using System.Text.Json.Nodes;

namespace Cblx.Dynamics.Explorer.Services.DynamicsServices.Metadata.ListEntitiesForMenu;

public class ListEntitiesForMenuHandler(ExplorerHttpClient client, DynamicsExplorerOptions options) : IListEntitiesForMenuHandler
{
    public async Task<EntityDto[]> GetAsync()
    {
        var jsonItems = await client.HttpClient
                    .GetFromJsonAsync<JsonObject>("""
                     EntityDefinitions?$select=LogicalName,DisplayName
                     """);

        var entities = jsonItems!["value"]!.AsArray().Select(item => new EntityDto
        {
            LogicalName = item!["LogicalName"]!.ToString()!,
            DisplayName = item["DisplayName"]?["LocalizedLabels"]?
                              .AsArray().FirstOrDefault()?["Label"]?
                              .GetValue<string>(),
            CustomName = options.Tables?.Find(t => t.Name == item["LogicalName"]!.ToString())?.FriendlyName
        })
            .Where(e => options.IgnoreTables == null
                        ||
                        !options.IgnoreTables(new IgnoreTableContext { LogicalName = e.LogicalName })
            )
            .OrderBy(e => e.CustomName == null)
            .ToArray();

        return entities;
    }
}