using Cblx.Dynamics.Explorer.Client.Services.DynamicsServices.Data.PatchItem;
using Cblx.Dynamics.Explorer.Client.Services.DynamicsServices.Data.PostItem;
using Cblx.Dynamics.Explorer.Client.Services.DynamicsServices.Metadata.GetEntity;
using System.Net.Http.Json;
using System.Text.Json.Nodes;

namespace Cblx.Dynamics.Explorer.Shared;

internal class EditDialogService(IGetEntityHandler getEntityHandler, IPostItem postItem, IPatchItem patchItem)
{ 
    public async Task PatchAsync(Guid id, string entitySetName, EditDialogSet[] sets)
    {
        var body = await PrepareBodyAsync(sets);
        if (!body.Any()) { return; }
        await patchItem.ExecuteAsync(new PatchItemRequest
        {
            Data = body,
            EntitySetName = entitySetName,
            Id = id
        });
    }

    public async Task PostAsync(string entitySetName, EditDialogSet[] sets)
    {
        var body = await PrepareBodyAsync(sets);
        if (!body.Any()) { return; }
        await postItem.ExecuteAsync(new PostItemRequest
        {
            Data = body,
            EntitySetName = entitySetName,
        });
        Console.WriteLine("IPA2");
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
