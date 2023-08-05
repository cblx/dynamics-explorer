using Cblx.Dynamics.Explorer.Services.DynamicsServices.ListTablesForMenu;
using Microsoft.Extensions.DependencyInjection;

namespace Cblx.Dynamics.Explorer.Services.DynamicsServices;

public static class DynamicsServicesExtensions
{
    public static IServiceCollection AddDynamicsServices(this IServiceCollection services)
    {
        services.AddScoped<IListEntitiesForMenuHandler, ListEntitiesForMenuHandler>();
        return services;
    }
}
