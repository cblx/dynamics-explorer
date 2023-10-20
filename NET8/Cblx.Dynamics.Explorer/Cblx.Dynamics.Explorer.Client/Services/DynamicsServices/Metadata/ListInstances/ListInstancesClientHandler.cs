using System.Net.Http.Json;

namespace Cblx.Dynamics.Explorer.Client.Services.DynamicsServices.Metadata.ListInstances;

public class ListInstancesClientHandler(HttpClient client) : IListInstancesHandler
{
    public async Task<InstanceGroupDto[]> ExecuteAsync()
        => (await client.GetFromJsonAsync<InstanceGroupDto[]>($"{Route.GetEndpoint<IListInstancesHandler>()}"))!;
}