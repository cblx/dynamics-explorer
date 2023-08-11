using Cblx.Dynamics.Explorer.Services.DynamicsServices.ExecuteQuery;
using Cblx.Dynamics.Explorer.Services.DynamicsServices.ListEntityAttributes;
using Cblx.Dynamics.Explorer.Services.DynamicsServices.ListTablesForMenu;
using Microsoft.Extensions.DependencyInjection;

namespace Cblx.Dynamics.Explorer.Services.DynamicsServices;

public static class DynamicsServicesExtensions
{
    public static IServiceCollection AddDynamicsServices(this IServiceCollection services)
    {
        services.AddScoped<IExecuteQueryHandler, ExecuteQueryHandler>();
        services.AddScoped<IGetEntityHandler, GetEntityHandler>();
        services.AddScoped<IListEntitiesForMenuHandler, ListEntitiesForMenuHandler>();
        return services;
    }
}
