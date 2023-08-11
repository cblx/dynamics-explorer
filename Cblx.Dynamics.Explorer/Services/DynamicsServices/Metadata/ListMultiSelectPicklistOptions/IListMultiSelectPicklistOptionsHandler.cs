using Cblx.Dynamics.Explorer.Models;

namespace Cblx.Dynamics.Explorer.Services.DynamicsServices.ListMultiSelectPicklistOptions
{
    internal interface IListMultiSelectPicklistOptionsHandler
    {
        Task<PicklistOption[]> GetAsync(string entityLogicalName, string attributeLogicalName);
    }
}