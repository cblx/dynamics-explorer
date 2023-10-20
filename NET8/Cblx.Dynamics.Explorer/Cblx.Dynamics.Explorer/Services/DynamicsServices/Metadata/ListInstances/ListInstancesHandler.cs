using Cblx.Dynamics.Explorer.Client.Services.DynamicsServices.Metadata.ListInstances;
using Cblx.Dynamics.Explorer.Services.Authenticator;

namespace Cblx.Dynamics.Explorer.Services.DynamicsServices.Metadata.ListInstances;

internal class ListInstancesHandler(DynamicsConfig[] instances) : IListInstancesHandler
{
    public Task<InstanceGroupDto[]> ExecuteAsync()
    {
        return Task.FromResult(instances.Select(i => i.Group).Distinct().Select(groupName => new InstanceGroupDto
        {
            Name = groupName,
            Instances = instances.Where(i => i.Group == groupName).Select(i => new InstanceDto { Name = i.Name }).ToArray()
        }).ToArray());
    }
}
