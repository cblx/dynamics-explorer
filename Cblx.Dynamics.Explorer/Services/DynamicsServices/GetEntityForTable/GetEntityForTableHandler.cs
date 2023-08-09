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
            EntityLogicalName = entityLogicalName,
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
        }).OrderBy(e => !e.IsPrimaryId)
          .ThenBy(e => e.CustomName == null)
          .ToArray();

        FillEditable(attributes);
        await FillDateTimeAttributeMetadataAsync(attributes, entityLogicalName);
        await FillRelationshipsMetadataAsync(attributes, entityLogicalName);

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

    private static void FillEditable(AttributeDto[] attributes)
    {
        foreach (var attribute in attributes)
        {
            attribute.IsEditable = !new string[]
          {
                "createdon",
                "importsequencenumber",
                "modifiedon",
                "overriddencreatedon",
                "timezoneruleversionnumber",
                "utcconversiontimezonecode",
                "versionnumber",
                "createdby",
                "createdonbehalfby",
                "modifiedby",
                "modifiedonbehalfby",
                "ownerid",
                "owningbusinessunit",
                "owninguser",
                "owningteam"
          }.Contains(attribute.LogicalName);
        }
    }

    private async Task FillRelationshipsMetadataAsync(AttributeDto[] attributes, string entityLogicalName)
    {
        if (!attributes.Exists(a => a.DerivedType == AttributeMetadataDerivedTypes.LookupAttributeMetadata)) { return; }
        var manyToOneRelationships = await client.HttpClient.GetFromJsonAsync<JsonObject>($"""
                EntityDefinitions(LogicalName='{entityLogicalName}')/ManyToOneRelationships?
                    $select=ReferencingAttribute,ReferencedEntity
                """);

        foreach(var relationship in manyToOneRelationships!["value"]!.AsArray())
        {
            var attribute = attributes.Find(a => a.LogicalName == relationship!["ReferencingAttribute"]!.ToString());
            if (attribute is null) { continue; }
            attribute.ReferencedEntity = relationship!["ReferencedEntity"]!.ToString();
        }
    }

    private async Task FillDateTimeAttributeMetadataAsync(AttributeDto[] attributes, string entityLogicalName)
    {
        if (!attributes.Exists(a => a.DerivedType == AttributeMetadataDerivedTypes.DateTimeAttributeMetadata)) { return; }
        var dateTimeAttributes = await client.HttpClient.GetFromJsonAsync<JsonObject>($"""
                EntityDefinitions(LogicalName='{entityLogicalName}')/Attributes/Microsoft.Dynamics.CRM.DateTimeAttributeMetadata?
                    $select=LogicalName,Format
                """);

        foreach (var dateTimeAttribute in dateTimeAttributes!["value"]!.AsArray())
        {
            var attribute = attributes.Find(a => a.LogicalName == dateTimeAttribute!["LogicalName"]!.ToString());
            if (attribute is null) { continue; }
            attribute.DateTimeFormat = Enum.Parse<DateTimeFormat>(dateTimeAttribute!["Format"]!.GetValue<string>());
        }
    }
}
