using Cblx.Dynamics.Explorer.Models;

namespace Cblx.Dynamics.Explorer.Services.DynamicsServices.Metadata.ListPicklistOptions;

internal interface IListPicklistOptionsHandler
{
    Task<PicklistOption[]> GetAsync(string entityLogicalName, string attributeLogicalName);
}