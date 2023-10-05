using Cblx.Dynamics.Explorer.Client;
using Cblx.Dynamics.Explorer.Client.Services.DynamicsServices.Data.PatchItem;
using Cblx.Dynamics.Explorer.Client.Services.DynamicsServices.Data.PostItem;
using Cblx.Dynamics.Explorer.Client.Services.DynamicsServices.Metadata.GetEntity;
using Cblx.Dynamics.Explorer.Services.DynamicsServices.Data.Delete;
using Cblx.Dynamics.Explorer.Services.DynamicsServices.ExecuteQuery;
using Cblx.Dynamics.Explorer.Services.DynamicsServices.ListMultiSelectPicklistOptions;
using Cblx.Dynamics.Explorer.Services.DynamicsServices.ListTablesForMenu;
using Cblx.Dynamics.Explorer.Services.DynamicsServices.Metadata.ListOptionsHandler;
using Cblx.Dynamics.Explorer.Services.DynamicsServices.Metadata.ListPicklistOptions;
using Cblx.Dynamics.Explorer.Services.DynamicsServices.Metadata.ListStateCodeOptions;
using Cblx.Dynamics.Explorer.Services.DynamicsServices.Metadata.ListStatusCodeOptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Cblx.Dynamics.Explorer.Services.DynamicsServices;

public static class Apis
{
    public static WebApplication MapDynamicsExplorerApis(this WebApplication app)
    {
        // Data
        app.MapDelete(
            Route.GetEndpoint<IDeleteHandler>(), 
            (IDeleteHandler handler, string entityLogicalName, Guid id) => handler.DeleteAsync(entityLogicalName, id)
        );
        app.MapGet(
            Route.GetEndpoint<IExecuteQueryHandler>(),
            (IExecuteQueryHandler handler, string query) => handler.GetAsync(query)
        );
        app.MapPatch(
            Route.GetEndpoint<IPatchItem>(),
            (IPatchItem handler, [FromBody]PatchItemRequest request) => handler.ExecuteAsync(request)
        );
        app.MapPost(
            Route.GetEndpoint<IPostItem>(),
            (IPostItem handler, [FromBody]PostItemRequest request) => handler.ExecuteAsync(request)
        );

        // Metadata
        app.MapGet(
            Route.GetEndpoint<IGetEntityHandler>(), 
            (IGetEntityHandler handler, string entityLogicalName) => handler.GetAsync(entityLogicalName)
        );

        app.MapGet(
            Route.GetEndpoint<IListEntitiesForMenuHandler>(),
            (IListEntitiesForMenuHandler handler) => handler.GetAsync()
        );

        app.MapGet(
            Route.GetEndpoint<IListMultiSelectPicklistOptionsHandler>(),
            (IListMultiSelectPicklistOptionsHandler handler, string entityLogicalName, string attributeLogicalName)
                => handler.GetAsync(entityLogicalName, attributeLogicalName)
        );
        app.MapGet(
            Route.GetEndpoint<IListOptionsHandler>(),
            (IListOptionsHandler handler, string entityLogicalName, string attributeLogicalName, string derivedTypeName)
                => handler.GetAsync(entityLogicalName, attributeLogicalName, derivedTypeName)
        );

        app.MapGet(
          Route.GetEndpoint<IListPicklistOptionsHandler>(),
          (IListPicklistOptionsHandler handler, string entityLogicalName, string attributeLogicalName)
              => handler.GetAsync(entityLogicalName, attributeLogicalName)
        );

        app.MapGet(
            Route.GetEndpoint<IListStateCodeOptionsHandler>(),
            (IListStateCodeOptionsHandler handler, string entityLogicalName)
                => handler.GetAsync(entityLogicalName)
        );

        app.MapGet(
            Route.GetEndpoint<IListStatusCodeOptionsHandler>(),
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
