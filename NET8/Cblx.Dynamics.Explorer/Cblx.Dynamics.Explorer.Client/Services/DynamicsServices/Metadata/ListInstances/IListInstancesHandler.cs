namespace Cblx.Dynamics.Explorer.Client.Services.DynamicsServices.Metadata.ListInstances;

public interface IListInstancesHandler
{
    Task<InstanceGroupDto[]> ExecuteAsync();
}
