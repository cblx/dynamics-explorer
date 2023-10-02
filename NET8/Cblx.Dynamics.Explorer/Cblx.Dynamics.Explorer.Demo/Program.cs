using Cblx.Dynamics;
using Cblx.Dynamics.Explorer;
using Cblx.Dynamics.Explorer.Models;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: true);
#if !DEBUG
builder.Services
    .AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"));
builder.Services.AddAuthorization(options => options.FallbackPolicy = options.DefaultPolicy);
#endif
builder.Services.AddDynamicsExplorer();
var options = new DynamicsExplorerOptions
{
    IgnoreTables = table => table.LogicalName.StartsWith("ms") 
                            || table.LogicalName.StartsWith("app")
                            || table.LogicalName.StartsWith("book")
                            || table.LogicalName.StartsWith("bot")
};
builder.Services.AddSingleton(options);
#if !DEBUG
builder.Services.AddServerSideBlazor().AddMicrosoftIdentityConsentHandler();
#endif
var app = builder.Build();
app.UseDynamicsExplorer();
app.Run();