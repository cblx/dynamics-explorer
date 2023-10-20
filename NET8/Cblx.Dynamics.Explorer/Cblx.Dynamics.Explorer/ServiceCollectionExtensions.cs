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
        var dynamicsConfig = configuration.GetSection("Dynamics").Get<DynamicsConfig>();

        var options = new DynamicsExplorerOptions
        {
            Tables = []
        };
        services.AddSingleton(options);
        services.AddMemoryCache();
        //services.AddSingleton<IDynamicsAuthenticator, DynamicsAuthenticator>();
        // Default Config, binding from "Dynamics" section
        //services
        //    .AddOptions<DynamicsConfig>()
        //    .Configure<IConfiguration>((dynamicsConfig, configuration) => configuration.GetSection("Dynamics").Bind(dynamicsConfig));
        //services.AddScoped(sp => sp.GetRequiredService<IOptions<DynamicsConfig>>().Value);
        if (dynamicsConfig != null)
        {
            services
                .AddHttpClient("default")
                .AddHttpMessageHandler(sp => new DynamicsAuthorizationMessageHandler(dynamicsConfig, sp.GetRequiredService<IMemoryCache>()))
                //.AddHttpMessageHandler<DynamicsAuthorizationMessageHandler>()
                .ConfigureHttpClient((sp, httpClient) =>
                {
                    httpClient.DefaultRequestHeaders.Add("Prefer", "odata.include-annotations=OData.Community.Display.V1.FormattedValue");
                    httpClient.BaseAddress = DynamicsBaseAddress.FromResourceUrl(dynamicsConfig.ResourceUrl);
                });
        }
        //services.AddTransient<DynamicsAuthorizationMessageHandler>();
        services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("default"));
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