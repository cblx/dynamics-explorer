using Cblx.Dynamics.Explorer.Client.Services.DynamicsServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Fast.Components.FluentUI;

namespace Cblx.Dynamics.Explorer.Client;

public static class ClientServices
{
    public static IServiceCollection AddClientServices(this IServiceCollection services, string baseAddress)
    {
        services.AddFluentUIComponents(options =>
        {
            options.HostingModel = BlazorHostingModel.WebAssembly;
        });
        services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(baseAddress) });
        services.AddDynamicsServices();
        return services;
    }
}
