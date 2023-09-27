using Cblx.Dynamics.Explorer.Components;
using Cblx.Dynamics.Explorer.Services.DynamicsServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace Cblx.Dynamics.Explorer;

public static class WebApplicationExtensions
{
    public static WebApplication UseDynamicsExplorer(this WebApplication app)
    {

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseWebAssemblyDebugging();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.MapRazorComponents<App>()
            .AddServerRenderMode()
            .AddWebAssemblyRenderMode()
            .AddAdditionalAssemblies(typeof(Client.Pages.Counter).Assembly);
        app.MapDynamicsExplorerApis();
        

        return app;
    }
}
