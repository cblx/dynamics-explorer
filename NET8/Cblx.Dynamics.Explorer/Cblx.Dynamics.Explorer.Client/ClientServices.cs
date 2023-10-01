using Cblx.Dynamics.Explorer.Client.Services.DynamicsServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Fast.Components.FluentUI;
using MudBlazor.Services;

namespace Cblx.Dynamics.Explorer.Client;

public static class ClientServices
{
    public static IServiceCollection AddClientServices(this IServiceCollection services, string baseAddress)
    {
        services.AddMudServices();
        services.AddFluentUIComponents();
        services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(baseAddress) });
        services.AddDynamicsServices();
        return services;
    }
}
