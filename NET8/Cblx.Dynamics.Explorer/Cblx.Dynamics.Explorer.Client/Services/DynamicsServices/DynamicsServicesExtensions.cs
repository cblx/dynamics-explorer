using Cblx.Dynamics.Explorer.Client.Services.DynamicsServices.Data.ListItems;
using Cblx.Dynamics.Explorer.Client.Services.DynamicsServices.Metadata.GetEntity;
using Cblx.Dynamics.Explorer.Client.Services.DynamicsServices.Metadata.ListEntitiesForMenu;
using Cblx.Dynamics.Explorer.Client.Services.DynamicsServices.Metadata.ListMultiSelectPicklistOptions;
using Cblx.Dynamics.Explorer.Client.Services.DynamicsServices.Metadata.ListOptionsHandler;
using Cblx.Dynamics.Explorer.Client.Services.DynamicsServices.Metadata.ListPicklistOptions;
using Cblx.Dynamics.Explorer.Client.Services.DynamicsServices.Metadata.ListStateCodeOptions;
using Cblx.Dynamics.Explorer.Client.Services.DynamicsServices.Metadata.ListStatusCodeOptions;
using Cblx.Dynamics.Explorer.Services.DynamicsServices.Data.Delete;
using Cblx.Dynamics.Explorer.Services.DynamicsServices.ExecuteQuery;
using Cblx.Dynamics.Explorer.Services.DynamicsServices.ListMultiSelectPicklistOptions;
using Cblx.Dynamics.Explorer.Services.DynamicsServices.ListTablesForMenu;
using Cblx.Dynamics.Explorer.Services.DynamicsServices.Metadata.ListOptionsHandler;
using Cblx.Dynamics.Explorer.Services.DynamicsServices.Metadata.ListPicklistOptions;
using Cblx.Dynamics.Explorer.Services.DynamicsServices.Metadata.ListStateCodeOptions;
using Cblx.Dynamics.Explorer.Services.DynamicsServices.Metadata.ListStatusCodeOptions;
using Microsoft.Extensions.DependencyInjection;

namespace Cblx.Dynamics.Explorer.Client.Services.DynamicsServices;

public static class DynamicsServicesExtensions
{
    public static IServiceCollection AddDynamicsServices(this IServiceCollection services)
    {
        services.AddScoped<IExecuteQueryHandler, ExecuteQueryClientHandler>();
        services.AddScoped<IGetEntityHandler, GetEntityClientHandler>();
        services.AddScoped<IListEntitiesForMenuHandler, ListEntitiesForMenuClientHandler>();
        services.AddScoped<IListMultiSelectPicklistOptionsHandler, ListMultiSelectPicklistOptionsClientHandler>();
        services.AddScoped<IListOptionsHandler, ListOptionsClientHandler>();
        services.AddScoped<IListPicklistOptionsHandler, ListPicklistOptionsClientHandler>();
        services.AddScoped<IListStateCodeOptionsHandler, ListStateCodeOptionsClientHandler>();
        services.AddScoped<IListStatusCodeOptionsHandler, ListStatusCodeOptionsClientHandler>();
        services.AddScoped<IDeleteHandler, DeleteClientHandler>();
        services.AddScoped<IListItems, ListItemsClient>();
        return services;
    }
}
