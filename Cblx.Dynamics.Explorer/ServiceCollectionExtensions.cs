using Cblx.Dynamics.AspNetCore;
using Cblx.Dynamics.Explorer.Services;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using OData.Client;
using OData.Client.Abstractions;
using System.Reflection;

namespace Cblx.Dynamics.Explorer;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDynamicsExplorer(this IServiceCollection services, params Assembly[] assemblies)
    {
        var options = new DynamicsExplorerOptions
        {
            Tables = assemblies
                .SelectMany(a => a.GetTypes())
                .Where(t => t.GetCustomAttribute<DynamicsEntityAttribute>() != null)
                .Select(t => new TableInfo
                {
                    Type = t,
                    Name = t.Name,
                    InternalName = t.GetCustomAttribute<DynamicsEntityAttribute>()!.Name
                })
                .ToArray()
        };
        services.AddSingleton(options);
        services.AddDynamics();
        services.AddScoped(sp => ((sp.GetRequiredService<IODataClient>() as ODataClient)!.Invoker as HttpClient)!);
        services.AddRazorPages();
        services.AddServerSideBlazor();
        services.AddMudServices();
        services.AddSingleton<ApplicationService>();
        services.AddSingleton<AppBarService>();
        services.AddScoped<SchemaService>();
        return services;
    }
}