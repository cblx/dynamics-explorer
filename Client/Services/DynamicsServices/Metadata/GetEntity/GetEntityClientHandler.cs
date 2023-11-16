using Cblx.Dynamics.Explorer.Client;
using System.Net.Http.Json;

namespace Cblx.Dynamics.Explorer.Client.Services.DynamicsServices.Metadata.GetEntity;

public class GetEntityClientHandler(HttpClient client) : IGetEntityHandler
{
    public async Task<EntityDto> GetAsync(string entityLogicalName)
        => (await client.GetFromJsonAsync<EntityDto>($"{Routes.GetEndpoint<IGetEntityHandler>()}?{nameof(entityLogicalName)}={entityLogicalName}"))!;
}
