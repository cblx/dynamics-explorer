using Cblx.Dynamics.Explorer.Services.DynamicsServices.ListTablesForMenu;

namespace Cblx.Dynamics.Explorer.Services.DynamicsServices;

public static class DynamicsServicesExtensions
{
    public static IServiceCollection AddDynamicsServices(this IServiceCollection services)
    {
        //services.AddScoped<IExecuteQueryHandler, ExecuteQueryHandler>();
        //services.AddScoped<IGetEntityHandler, GetEntityHandler>();
        services.AddScoped<IListEntitiesForMenuHandler, ListEntitiesForMenuHandler>();
        //services.AddScoped<IListMultiSelectPicklistOptionsHandler, ListMultiSelectPicklistOptionsHandler>();
        //services.AddScoped<IListOptionsHandler, ListOptionsHandler>();
        //services.AddScoped<IListPicklistOptionsHandler, ListPicklistOptionsHandler>();
        //services.AddScoped<IListStateCodeOptionsHandler, ListStateCodeOptionsHandler>();
        //services.AddScoped<IListStatusCodeOptionsHandler, ListStatusCodeOptionsHandler>();
        //services.AddScoped<IDeleteHandler, DeleteHandler>();
        return services;
    }
}
