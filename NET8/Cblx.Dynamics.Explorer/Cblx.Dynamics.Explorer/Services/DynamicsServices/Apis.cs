using Cblx.Dynamics.Explorer.Client;
using Cblx.Dynamics.Explorer.Client.Services.DynamicsServices.Metadata.GetEntity;
using Cblx.Dynamics.Explorer.Services.DynamicsServices.Data.Delete;
using Cblx.Dynamics.Explorer.Services.DynamicsServices.ExecuteQuery;
using Cblx.Dynamics.Explorer.Services.DynamicsServices.ListTablesForMenu;
using Microsoft.AspNetCore.Builder;

namespace Cblx.Dynamics.Explorer.Services.DynamicsServices;

public static class Apis
{
    public static WebApplication MapDynamicsExplorerApis(this WebApplication app)
    {
        // Data
        app.MapDelete(
            Route.GetEndpoint<IDeleteHandler>(), 
            (IDeleteHandler handler, string entityLogicalName, Guid id)
                => handler.DeleteAsync(entityLogicalName, id)
        );
        app.MapGet(
            Route.GetEndpoint<IExecuteQueryHandler>(),
            (IExecuteQueryHandler handler, string query)
                => handler.GetAsync(query)
        );

        // Metadata
        app.MapGet(
            Route.GetEndpoint<IGetEntityHandler>(), 
            (IGetEntityHandler handler, string entityLogicalName)
                => handler.GetAsync(entityLogicalName)
        );

        app.MapGet(
            Route.GetEndpoint<IListEntitiesForMenuHandler>(),
            (IListEntitiesForMenuHandler handler)
                => handler.GetAsync()
        );
        return app;
    }
}
