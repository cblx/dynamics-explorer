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
        Console.WriteLine("APA");
        var body = await PrepareBodyAsync(sets);
        Console.WriteLine("EPA");
        if (!body.Any()) { return; }
        Console.WriteLine("OPA");
        await patchItem.ExecuteAsync(new PatchItemRequest
        {
            Data = body,
            EntitySetName = entitySetName,
            Id = id
        });
        Console.WriteLine("IPA");
    }

    public async Task PostAsync(string entitySetName, EditDialogSet[] sets)
    {
        Console.WriteLine("APA2");
        var body = await PrepareBodyAsync(sets);
        Console.WriteLine("EPA2");
        if (!body.Any()) { return; }
        Console.WriteLine("OPA2");
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
