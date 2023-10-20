using Cblx.Dynamics.Explorer.Services;
using Cblx.Dynamics.Explorer.Services.Authenticator;
using Cblx.Dynamics.Explorer.Services.DynamicsServices;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Fast.Components.FluentUI;

namespace Cblx.Dynamics.Explorer;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDynamicsExplorer(this IServiceCollection services)
    {
        var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
        var defaultConfig = configuration.GetSection("Dynamics").Get<DynamicsConfig>();
        var instances = configuration.GetSection("Instances").Get<DynamicsConfig[]>();
        var options = new DynamicsExplorerOptions
        {
            Tables = []
        };
        services.AddHttpContextAccessor();
        services.AddSingleton(options);
        services.AddMemoryCache();
        if (defaultConfig != null)
        {
            services.AddHttpClientForInstance(defaultConfig);
        }
        instances?.ToList().ForEach(instance => services.AddHttpClientForInstance(instance));
        services.AddKeyedScoped("dynamics.explorer", (sp, _key) =>
        {
            return sp.GetRequiredService<IHttpClientFactory>().CreateClient(defaultConfig!.Key);
        });
        services.AddRazorComponents()
                   .AddServerComponents()
                   .AddWebAssemblyComponents();
        services.AddFluentUIComponents();
        services.AddDynamicsServices();
        services.AddScoped<ExplorerHttpClient>();
        services.AddSingleton<ApplicationService>();
        return services;
    }

    private static IServiceCollection AddHttpClientForInstance(this IServiceCollection services, DynamicsConfig instance)
    {
        services
               .AddHttpClient(instance.Key)
               .AddHttpMessageHandler(sp => new DynamicsAuthorizationMessageHandler(instance, sp.GetRequiredService<IMemoryCache>()))
               .ConfigureHttpClient((sp, httpClient) =>
               {
                   httpClient.DefaultRequestHeaders.Add("Prefer", "odata.include-annotations=OData.Community.Display.V1.FormattedValue");
                   httpClient.BaseAddress = DynamicsBaseAddress.FromResourceUrl(instance.ResourceUrl);
               });
        return services;
    }
}