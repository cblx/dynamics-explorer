using System.Net.Http.Json;
using System.Text.Json.Nodes;

namespace Cblx.Dynamics.Explorer.Services.DynamicsServices.ListEntityAttributes;

public class GetEntityForTableHandler(ExplorerHttpClient client, DynamicsExplorerOptions options) : IGetEntityForTableHandler
{
    public async Task<EntityDto> GetAsync(string entityLogicalName)
    {
        var jsonObject = await client.HttpClient
                   .GetFromJsonAsync<JsonObject>($"""
                     EntityDefinitions(LogicalName='{entityLogicalName}')?
                        $select=LogicalName,DisplayName,EntitySetName
                        &$expand=Attributes($select=LogicalName,DisplayName,IsPrimaryId,IsPrimaryName,AttributeType)
                     """.RemoveLineEndingsForODataQuery());
        var jsonAttributes = jsonObject!["Attributes"]!.AsArray();
        var attributes = jsonAttributes.Select(item => new AttributeDto
        {
            IsPrimaryId = item!["IsPrimaryId"]!.GetValue<bool>(),
            IsPrimaryName = item!["IsPrimaryName"]!.GetValue<bool>(),
            LogicalName = item!["LogicalName"]!.ToString()!,
            DisplayName = item["DisplayName"]?["LocalizedLabels"]?
                              .AsArray().FirstOrDefault()?["Label"]?
                              .GetValue<string>(),
            CustomName = options.Tables?
                                .Find(t => t.Name == entityLogicalName)?
                                .Columns?.Find(c => c.Name == item["LogicalName"]!.ToString())?
                                .FriendlyName,
            AttributeType = item["AttributeType"]!.GetValue<string>()!,
            DerivedType = item["@odata.type"]?.GetValue<string>()
        })
            .OrderBy(e => !e.IsPrimaryId)
            .ThenBy(e => e.CustomName == null)
            .ToArray();

        var entity = new EntityDto
        {
            EntitySetName = jsonObject!["EntitySetName"]!.ToString()!,
            LogicalName = jsonObject!["LogicalName"]!.ToString()!,
            DisplayName = jsonObject["DisplayName"]?["LocalizedLabels"]?
                              .AsArray().FirstOrDefault()?["Label"]?
                              .GetValue<string>(),
            CustomName = options.Tables?.Find(t => t.Name == jsonObject!["LogicalName"]!.ToString())?.FriendlyName,
            Attributes = attributes
        };

        return entity;
    }
}
