using Cblx.Dynamics.Explorer.Models;

namespace Cblx.Dynamics.Explorer.Services.DynamicsServices.Metadata.ListStateCodeOptions;

internal interface IListStateCodeOptionsHandler
{
    Task<PicklistOption[]> GetAsync(string entityLogicalName);
}