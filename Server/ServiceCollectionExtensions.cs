using Cblx.Dynamics.Explorer.Client.Services;
using Cblx.Dynamics.Explorer.Services;
using Cblx.Dynamics.Explorer.Services.Authenticator;
using Cblx.Dynamics.Explorer.Services.DynamicsServices;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.FluentUI.AspNetCore.Components;

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
        services.AddSingleton<InstanceContextService>();
        services.AddSingleton(options);
        services.AddScoped<UserContext>();
        services.AddMemoryCache();
        instances?.ToList().ForEach(instance => services.AddHttpClientForInstance(instance));
        services.AddSingleton(instances ?? []);
        services.AddScoped(sp =>
        {
            var httpContext = sp.GetRequiredService<IHttpContextAccessor>().HttpContext!;
            var instance = httpContext.Request.Headers["x-Dynamics-Explorer-Instance"]!;
            var group = httpContext.Request.Headers["x-Dynamics-Explorer-Group"]!;
            var config = instances!.Find(i => i.Name == instance && i.Group == group);
            if(config is null) { return new DynamicsConfig(); }
            return config;
        });
        services.AddKeyedScoped("dynamics.explorer", (sp, _key) =>
        {
            var currentInstance = sp.GetRequiredService<DynamicsConfig>();
            var key = DynamicsConfig.CreateKey(currentInstance.Group, currentInstance.Name);
            return sp.GetRequiredService<IHttpClientFactory>().CreateClient(key);
        });
        services.AddRazorComponents()
                   .AddInteractiveServerComponents()
                   .AddInteractiveWebAssemblyComponents();
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