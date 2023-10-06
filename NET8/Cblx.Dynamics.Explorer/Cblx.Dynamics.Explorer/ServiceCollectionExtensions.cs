using Cblx.Dynamics.Explorer.Models;
using Cblx.Dynamics.Explorer.Services;
using Cblx.Dynamics.Explorer.Services.Authenticator;
using Cblx.Dynamics.Explorer.Services.DynamicsServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Fast.Components.FluentUI;

namespace Cblx.Dynamics.Explorer;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDynamicsExplorer(this IServiceCollection services)
    {
        var options = new DynamicsExplorerOptions
        {
            Tables = Array.Empty<TableOptions>()
        };
        services.AddSingleton(options);
        services.AddSingleton<IDynamicsAuthenticator, DynamicsAuthenticator>();
        // Default Config, binding from "Dynamics" section
        services
            .AddOptions<DynamicsConfig>()
            .Configure<IConfiguration>((dynamicsConfig, configuration) => configuration.GetSection("Dynamics").Bind(dynamicsConfig));
        services.AddScoped(sp => sp.GetRequiredService<IOptions<DynamicsConfig>>().Value);
        services
            .AddHttpClient("IODataClient")
            .AddHttpMessageHandler<DynamicsAuthorizationMessageHandler>()
            .ConfigureHttpClient((sp, httpClient) =>
            {
                var dynamicsConfig = sp.GetRequiredService<IOptions<DynamicsConfig>>().Value;
                httpClient.DefaultRequestHeaders.Add("Prefer", "odata.include-annotations=OData.Community.Display.V1.FormattedValue");
                httpClient.BaseAddress = DynamicsBaseAddress.FromResourceUrl(dynamicsConfig.ResourceUrl);
            });
        services.AddTransient<DynamicsAuthorizationMessageHandler>();
        services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("IODataClient"));
        services.AddRazorComponents()
                   .AddServerComponents()
                   .AddWebAssemblyComponents();
        services.AddFluentUIComponents();
        services.AddDynamicsServices();
        services.AddScoped<ExplorerHttpClient>();

       
        services.AddSingleton<ApplicationService>();
        return services;
    }
}