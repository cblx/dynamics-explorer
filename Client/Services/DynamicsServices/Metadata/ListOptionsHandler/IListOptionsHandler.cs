using Cblx.Dynamics.Explorer.Models;

namespace Cblx.Dynamics.Explorer.Services.DynamicsServices.Metadata.ListOptionsHandler
{
    internal interface IListOptionsHandler
    {
        Task<PicklistOption[]> GetAsync(string entityLogicalName, string attributeLogicalName, string derivedTypeName);
    }
}