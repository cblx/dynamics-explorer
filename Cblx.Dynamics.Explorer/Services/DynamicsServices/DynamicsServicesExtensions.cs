using Cblx.Dynamics.Explorer.Services.DynamicsServices.Data.Delete;
using Cblx.Dynamics.Explorer.Services.DynamicsServices.ExecuteQuery;
using Cblx.Dynamics.Explorer.Services.DynamicsServices.ListEntityAttributes;
using Cblx.Dynamics.Explorer.Services.DynamicsServices.ListMultiSelectPicklistOptions;
using Cblx.Dynamics.Explorer.Services.DynamicsServices.ListTablesForMenu;
using Cblx.Dynamics.Explorer.Services.DynamicsServices.Metadata.ListMultiSelectPicklistOptions;
using Cblx.Dynamics.Explorer.Services.DynamicsServices.Metadata.ListOptionsHandler;
using Cblx.Dynamics.Explorer.Services.DynamicsServices.Metadata.ListPicklistOptions;
using Cblx.Dynamics.Explorer.Services.DynamicsServices.Metadata.ListStateCodeOptions;
using Cblx.Dynamics.Explorer.Services.DynamicsServices.Metadata.ListStatusCodeOptions;
using Microsoft.Extensions.DependencyInjection;

namespace Cblx.Dynamics.Explorer.Services.DynamicsServices;

public static class DynamicsServicesExtensions
{
    public static IServiceCollection AddDynamicsServices(this IServiceCollection services)
    {
        services.AddScoped<IExecuteQueryHandler, ExecuteQueryHandler>();
        services.AddScoped<IGetEntityHandler, GetEntityHandler>();
        services.AddScoped<IListEntitiesForMenuHandler, ListEntitiesForMenuHandler>();
        services.AddScoped<IListMultiSelectPicklistOptionsHandler, ListMultiSelectPicklistOptionsHandler>();
        services.AddScoped<IListOptionsHandler, ListOptionsHandler>();
        services.AddScoped<IListPicklistOptionsHandler, ListPicklistOptionsHandler>();
        services.AddScoped<IListStateCodeOptionsHandler, ListStateCodeOptionsHandler>();
        services.AddScoped<IListStatusCodeOptionsHandler, ListStatusCodeOptionsHandler>();
        services.AddScoped<IDeleteHandler, DeleteHandler>();
        return services;
    }
}
