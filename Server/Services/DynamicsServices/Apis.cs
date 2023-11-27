using Cblx.Dynamics.Explorer.Client;
using Cblx.Dynamics.Explorer.Client.Services.DynamicsServices.Data.PatchItem;
using Cblx.Dynamics.Explorer.Client.Services.DynamicsServices.Data.PostItem;
using Cblx.Dynamics.Explorer.Client.Services.DynamicsServices.Metadata.GetEntity;
using Cblx.Dynamics.Explorer.Client.Services.DynamicsServices.Metadata.ListInstances;
using Cblx.Dynamics.Explorer.Services.DynamicsServices.Data.Delete;
using Cblx.Dynamics.Explorer.Services.DynamicsServices.ExecuteQuery;
using Cblx.Dynamics.Explorer.Services.DynamicsServices.ListMultiSelectPicklistOptions;
using Cblx.Dynamics.Explorer.Services.DynamicsServices.ListTablesForMenu;
using Cblx.Dynamics.Explorer.Services.DynamicsServices.Metadata.ListOptionsHandler;
using Cblx.Dynamics.Explorer.Services.DynamicsServices.Metadata.ListPicklistOptions;
using Cblx.Dynamics.Explorer.Services.DynamicsServices.Metadata.ListStateCodeOptions;
using Cblx.Dynamics.Explorer.Services.DynamicsServices.Metadata.ListStatusCodeOptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Cblx.Dynamics.Explorer.Services.DynamicsServices;

public static class Apis
{
    public static WebApplication MapDynamicsExplorerApis(this WebApplication app)
    {
        // Data
        app.MapDelete(
            Routes.GetEndpoint<IDeleteHandler>(), 
            (IDeleteHandler handler, string entityLogicalName, Guid id) => handler.DeleteAsync(entityLogicalName, id)
        );
        app.MapGet(
            Routes.GetEndpoint<IExecuteQueryHandler>(),
            (IExecuteQueryHandler handler, string query) => handler.GetAsync(query)
        );
        app.MapPatch(
            Routes.GetEndpoint<IPatchItem>(),
            (IPatchItem handler, [FromBody]PatchItemRequest request) => handler.ExecuteAsync(request)
        );
        app.MapPost(
            Routes.GetEndpoint<IPostItem>(),
            (IPostItem handler, [FromBody]PostItemRequest request) => handler.ExecuteAsync(request)
        );

        // Metadata
        app.MapGet(
            Routes.GetEndpoint<IGetEntityHandler>(), 
            (IGetEntityHandler handler, string entityLogicalName) => handler.GetAsync(entityLogicalName)
        );

        app.MapGet(
            Routes.GetEndpoint<IListEntitiesForMenuHandler>(),
            (IListEntitiesForMenuHandler handler) => handler.GetAsync()
        );

        app.MapGet(
            Routes.GetEndpoint<IListInstancesHandler>(),
            (IListInstancesHandler handler, HttpContext httpContext) => handler.ExecuteAsync()
        );

        app.MapGet(
            Routes.GetEndpoint<IListMultiSelectPicklistOptionsHandler>(),
            (IListMultiSelectPicklistOptionsHandler handler, string entityLogicalName, string attributeLogicalName)
                => handler.GetAsync(entityLogicalName, attributeLogicalName)
        );
        app.MapGet(
            Routes.GetEndpoint<IListOptionsHandler>(),
            (IListOptionsHandler handler, string entityLogicalName, string attributeLogicalName, string derivedTypeName)
                => handler.GetAsync(entityLogicalName, attributeLogicalName, derivedTypeName)
        );

        app.MapGet(
          Routes.GetEndpoint<IListPicklistOptionsHandler>(),
          (IListPicklistOptionsHandler handler, string entityLogicalName, string attributeLogicalName)
              => handler.GetAsync(entityLogicalName, attributeLogicalName)
        );

        app.MapGet(
            Routes.GetEndpoint<IListStateCodeOptionsHandler>(),
            (IListStateCodeOptionsHandler handler, string entityLogicalName)
                => handler.GetAsync(entityLogicalName)
        );

        app.MapGet(
            Routes.GetEndpoint<IListStatusCodeOptionsHandler>(),
            (IListStatusCodeOptionsHandler handler, string entityLogicalName)
                => handler.GetAsync(entityLogicalName)
        );
        app.UseExceptionHandler(app => app.Run(async context =>
        {
            var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>()!;
            var exception = exceptionHandlerPathFeature.Error;
            if(exception is InvalidOperationException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            string error = exception.Message;
            await context.Response.WriteAsJsonAsync(new
            {
                error,
                type = "application"
            });
            await context.Response.CompleteAsync();
        }));
        return app;
    }
}
