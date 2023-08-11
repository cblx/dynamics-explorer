using Cblx.Dynamics.Explorer.Services.DynamicsServices.ListEntityAttributes;
using System.Net.Http.Json;
using System.Text.Json.Nodes;

namespace Cblx.Dynamics.Explorer.Shared;

internal class EditDialogService(HttpClient httpClient, IGetEntityHandler getEntityHandler)
{ 
    public async Task PatchAsync(Guid id, string entitySetName, EditDialogSet[] sets)
    {
        var body = await PrepareBodyAsync(sets.Where(s => s.IsDirty()).ToArray());
        if (!body.Any()) { return; }
        await ManageResponseAsync(await httpClient.PatchAsJsonAsync($"{entitySetName}({id})", body));
    }

    public async Task PostAsync(string entitySetName, EditDialogSet[] sets)
    {
        var body = await PrepareBodyAsync(sets);
        if (!body.Any()) { return; }
        await ManageResponseAsync(await httpClient.PostAsJsonAsync(entitySetName, body));
    }

    private static async Task ManageResponseAsync(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            var errorJson = await response.Content.ReadFromJsonAsync<JsonObject>();
            throw new InvalidOperationException(errorJson!["error"]!["message"]!.ToString());
        }
    }

    private async Task<Dictionary<string, object?>> PrepareBodyAsync(EditDialogSet[] sets)
    {
        var fields = new Dictionary<string, object?>();
        foreach (var set in sets)
        {
            if (set.Attribute!.DerivedType == AttributeMetadataDerivedTypes.LookupAttributeMetadata)
            {
                if (set.Value is null)
                {
                    fields[$"{set.Attribute!.LogicalName}@odata.bind"] = null;
                }
                else
                {
                    var referencedEntity = await getEntityHandler.GetAsync(set.Attribute.ReferencedEntity!);
                    fields[$"{set.Attribute!.LogicalName}@odata.bind"] = $"{referencedEntity.EntitySetName}({set.Value})";
                }
            }
            else
            {
                fields[set.Attribute!.LogicalName] = set.Value;
            }
        }
        return fields;
    }
}
