using Cblx.Dynamics.Explorer.Client.Services.DynamicsServices.Metadata.ListInstances;
using Cblx.Dynamics.Explorer.Services.Authenticator;

namespace Cblx.Dynamics.Explorer.Services.DynamicsServices.Metadata.ListInstances;

internal class ListInstancesHandler(DynamicsConfig[] instances, UserContext userContext) : IListInstancesHandler
{
    public Task<InstanceDto[]> ExecuteAsync()
    {
        var result = instances.Select(i => new InstanceDto
        {
            Group = i.Group,
            Name = i.Name,
            Access = i.Users.Find(u => u.Login == userContext.Login)?.Access ?? i.DefaultAccess,
        }).Where(i => i.Access is DynamicsAccess.Write or DynamicsAccess.Read or DynamicsAccess.Know)
          .ToArray();

        return Task.FromResult(result);
    }
}
