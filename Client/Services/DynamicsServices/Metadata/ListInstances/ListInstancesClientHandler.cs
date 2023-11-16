using System.Net.Http.Json;

namespace Cblx.Dynamics.Explorer.Client.Services.DynamicsServices.Metadata.ListInstances;

public class ListInstancesClientHandler(HttpClient client) : IListInstancesHandler
{
    public async Task<InstanceDto[]> ExecuteAsync()
        => (await client.GetFromJsonAsync<InstanceDto[]>($"{Routes.GetEndpoint<IListInstancesHandler>()}"))!;
}