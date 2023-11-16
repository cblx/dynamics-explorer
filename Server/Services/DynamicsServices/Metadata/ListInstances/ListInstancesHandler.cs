using Cblx.Dynamics.Explorer.Client.Services.DynamicsServices.Metadata.ListInstances;
using Cblx.Dynamics.Explorer.Services.Authenticator;

namespace Cblx.Dynamics.Explorer.Services.DynamicsServices.Metadata.ListInstances;

internal class ListInstancesHandler(DynamicsConfig[] instances) : IListInstancesHandler
{
    public Task<InstanceDto[]> ExecuteAsync()
    {
        return Task.FromResult(instances.Select(i => new InstanceDto {
            Group = i.Group,
            Name = i.Name
        }).ToArray());
    }
}
