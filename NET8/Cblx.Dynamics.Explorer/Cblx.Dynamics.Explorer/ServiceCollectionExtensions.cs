using Cblx.Dynamics.Explorer.Services;
using Cblx.Dynamics.Explorer.Services.Authenticator;
using Cblx.Dynamics.Explorer.Services.DynamicsServices;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Fast.Components.FluentUI;
using System.Net.Http;

namespace Cblx.Dynamics.Explorer;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDynamicsExplorer(this IServiceCollection services)
    {
        var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
        var instances = configuration.GetSection("Instances").Get<DynamicsConfig[]>();
        var options = new DynamicsExplorerOptions
        {
            Tables = []
        };
        services.AddHttpContextAccessor();
        services.AddSingleton(options);
        services.AddMemoryCache();
        instances?.ToList().ForEach(instance => services.AddHttpClientForInstance(instance));
        services.AddSingleton(instances ?? []);
        services.AddKeyedScoped("dynamics.explorer", (sp, _key) =>
        {
            var httpContext = sp.GetRequiredService<IHttpContextAccessor>().HttpContext!;
            var key = DynamicsConfig.CreateKey(
                httpContext.Request.Headers["x-Dynamics-Explorer-Group"]!,
                httpContext.Request.Headers["x-Dynamics-Explorer-Instance"]!
            );
            return sp.GetRequiredService<IHttpClientFactory>().CreateClient(key);
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