using Cblx.Dynamics;
using Cblx.Dynamics.Explorer;
using Cblx.Dynamics.Explorer.Models;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;
using System.Reflection;
using System.Text.Json.Serialization;
using static MudBlazor.CategoryTypes;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: true);
#if !DEBUG
builder.Services
    .AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"));
builder.Services.AddAuthorization(options => options.FallbackPolicy = options.DefaultPolicy);
#endif
builder.Services.AddDynamicsExplorer();
string[] ignored = [
    "aaduser",
    "suggestioncardtemplate",
    "supportusertable",
    "teammembership",
    "teamprofiles",
    "teamroles",
    "teamsyncattributemappingprofiles",
    "timestampdatemapping",
    "traceassociation",
    "traceregarding",
    "unresolvedaddress",
    "untrackedemail",
    "userapplicationmetadata",
    "usersearchfacet",
    "virtualresourcegroupresource",
    "workflowwaitsubscription"
];
var options = new DynamicsExplorerOptions
{
    IgnoreTables = table => string.IsNullOrWhiteSpace(table.DisplayName)
                            || ignored.Contains(table.LogicalName)
                            || table.LogicalName.StartsWith("adx")
                            || table.LogicalName.StartsWith("adx")
                            || table.LogicalName.StartsWith("app")
                            || table.LogicalName.StartsWith("book")
                            || table.LogicalName.StartsWith("bot")
                            || table.LogicalName.StartsWith("ms")
                            || table.LogicalName.StartsWith("subscription")
                            || table.LogicalName.StartsWith("synap")
                            || table.LogicalName.StartsWith("system")
                            || table.LogicalName.Contains("plugin")
                            
};
builder.Services.AddSingleton(options);
#if !DEBUG
builder.Services.AddServerSideBlazor().AddMicrosoftIdentityConsentHandler();
#endif
var app = builder.Build();
app.UseDynamicsExplorer();
app.Run();