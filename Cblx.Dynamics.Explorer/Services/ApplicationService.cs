using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OData.Client;
using OData.Client.Abstractions;
using System.Net.Http.Json;
using System.Text.Json.Nodes;

namespace Cblx.Dynamics.Explorer.Services;

public class ApplicationService
{
    private readonly IServiceProvider _provider;
    private readonly Lazy<Task<Guid[]>> _applicationUsersIds;

    public ApplicationService(IServiceProvider provider, IConfiguration configuration)
    {
        _provider = provider;
        _applicationUsersIds = new Lazy<Task<Guid[]>>(async () =>
        {
            var services = _provider.CreateScope().ServiceProvider;
            var oDataClient = services.GetRequiredService<IODataClient>() as ODataClient;
            var httpClient = oDataClient!.Invoker as HttpClient;
            var dynamicsApplicationIds = configuration["Dynamics:Users"]!.ToString()
                .Split(",")
                .Select(userAndSecret => userAndSecret.Split(':').First())
                .ToArray();
            string filter = string.Join(" or ", dynamicsApplicationIds.Select(appId => $"applicationid eq {appId}"));
            var jsonObject = await httpClient!.GetFromJsonAsync<JsonObject>($"systemusers?$filter={filter}&$select=systemuserid");
            return jsonObject!["value"]!.AsArray()
                .Cast<JsonObject>()
                .Select(node => node["systemuserid"]!.GetValue<Guid>())
                .ToArray();
        });
    }

    public async Task<Guid[]> GetApplicationIdsAsync() => await _applicationUsersIds.Value;
}
