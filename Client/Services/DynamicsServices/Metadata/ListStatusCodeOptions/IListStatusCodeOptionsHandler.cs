using Cblx.Dynamics.Explorer.Models;

namespace Cblx.Dynamics.Explorer.Services.DynamicsServices.Metadata.ListStatusCodeOptions;

internal interface IListStatusCodeOptionsHandler
{
    Task<PicklistOption[]> GetAsync(string entityLogicalName);
}