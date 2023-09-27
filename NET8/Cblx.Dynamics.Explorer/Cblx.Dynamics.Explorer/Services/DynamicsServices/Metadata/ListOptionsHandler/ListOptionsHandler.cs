using Cblx.Dynamics.Explorer.Models;
using Cblx.Dynamics.Explorer.Services.DynamicsServices.ListMultiSelectPicklistOptions;
using Cblx.Dynamics.Explorer.Services.DynamicsServices.Metadata.ListPicklistOptions;
using Cblx.Dynamics.Explorer.Services.DynamicsServices.Metadata.ListStateCodeOptions;
using Cblx.Dynamics.Explorer.Services.DynamicsServices.Metadata.ListStatusCodeOptions;

namespace Cblx.Dynamics.Explorer.Services.DynamicsServices.Metadata.ListOptionsHandler;
using Types = AttributeMetadataDerivedTypes;
internal class ListOptionsHandler(
    IListMultiSelectPicklistOptionsHandler listMultiSelectPicklistOptionsHandler,
    IListPicklistOptionsHandler listPicklistOptionsHandler,
    IListStateCodeOptionsHandler listStateCodeOptionsHandler,
    IListStatusCodeOptionsHandler listStatusCodeOptionsHandler
) : IListOptionsHandler
{
    public async Task<PicklistOption[]> GetAsync(string entityLogicalName, string attributeLogicalName, string derivedTypeName)
    {
        return derivedTypeName switch
        {
            Types.MultiSelectPicklistAttributeMetadata => await listMultiSelectPicklistOptionsHandler.GetAsync(entityLogicalName, attributeLogicalName),
            Types.PicklistAttributeMetadata => await listPicklistOptionsHandler.GetAsync(entityLogicalName, attributeLogicalName),
            Types.StateAttributeMetadata => await listStateCodeOptionsHandler.GetAsync(entityLogicalName),
            Types.StatusAttributeMetadata => await listStatusCodeOptionsHandler.GetAsync(entityLogicalName),
            _ => throw new NotSupportedException($"Unsupported attribute type: {derivedTypeName}")
        };
    }
}
