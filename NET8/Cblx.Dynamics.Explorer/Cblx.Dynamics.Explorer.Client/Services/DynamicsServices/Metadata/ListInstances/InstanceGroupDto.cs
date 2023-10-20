namespace Cblx.Dynamics.Explorer.Client.Services.DynamicsServices.Metadata.ListInstances;

public class InstanceGroupDto
{
    public required string Name { get; set; }
    public required InstanceDto[] Instances { get; set; }
}
