using Cblx.Dynamics;
using Cblx.Dynamics.Explorer;
using Cblx.Dynamics.Explorer.Models;
using Cblx.Dynamics.Explorer.Services.Authenticator;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Web;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: true);

builder.Configuration.AddAzureAppConfiguration("", optional: true);
//builder.Configuration.AddAzureAppConfiguration(a => { 
//    a.Connect().
//});
var teste = builder.Configuration.GetValue<Teste>("Teste");

Console.WriteLine(JsonSerializer.Serialize(teste));
Console.WriteLine(builder.Configuration["Teste"]);

var cfg = builder.Configuration["Instances"];
var instances = builder.Configuration.GetSection("Instances").Get<DynamicsConfig[]>();
Console.WriteLine(cfg);
Console.WriteLine(instances?.Length ?? -1);
Console.WriteLine(builder.Configuration["Teste"]);
#if !DEBUG
builder.Services
    .AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"));
builder.Services.AddAuthorization(options => options.FallbackPolicy = options.DefaultPolicy);
#endif
builder.Services.AddDynamicsExplorer();
string[] ignored = [
    "aaduser",
    "accountleads",
    "aciviewmapper",
    "adminsettingsentity",
    "advancedsimilarityrule",
    "apisettings",
    "asyncoperation",
    "audit",
    "authorizationserver",
    "azureserviceconnection",
    "backgroundoperation",
    "callbackregistration",
    "canvasapp",
    "category",
    "clientupdate",
    "constraintbasedgroup",
    "delegatedauthorization",
    "delveactionhub",
    "enablearchivalrequest",
    "equipment",
    "fax",
    "feedback",
    "fileattachment",
    "filtertemplate",
    "fxexpression",
    "globalsearchconfiguration",
    "constraintbasegroup",
    "holidaywrapper",
    "imagedescriptor",
    "indexattributes",
    "integrationstatus",
    "invaliddependency",
    "isvconfig",
    "license",
    "letter",
    "localconfigstore",
    "keyvaultreference",
    "marketingformdisplayattributes",
    "metric",
    "newprocess",
    "nlsqregistration",
    "notification",
    "offlinecommanddefinition",
    "optionset",
    "orderclose",
    "package",
    "partnerapplication",
    "personaldocumenttemplate",
    "pricelevel",
    "replicationbacklog",
    "retaineddataexcel",
    "revokeinheritedaccessrecordstracker",
    "settingdefinition",
    "similarityrule",
    "site",
    "suggestioncardtemplate",
    "supportusertable",
    "task",
    "template",
    "territory",
    "theme",
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
                            || table.LogicalName.StartsWith("activity")
                            || table.LogicalName.StartsWith("action")
                            || table.LogicalName.StartsWith("app")
                            || table.LogicalName.StartsWith("archive") 
                            || table.LogicalName.StartsWith("attribute")
                            || table.LogicalName.StartsWith("available")
                            || table.LogicalName.StartsWith("book")
                            || table.LogicalName.StartsWith("bot")
                            || table.LogicalName.StartsWith("bulk")
                            || table.LogicalName.StartsWith("business")
                            || table.LogicalName.StartsWith("calendar")
                            || table.LogicalName.StartsWith("call")
                            || table.LogicalName.StartsWith("campaign")
                            || table.LogicalName.StartsWith("card")
                            || table.LogicalName.StartsWith("cascade")
                            || table.LogicalName.StartsWith("catalog")
                            || table.LogicalName.StartsWith("ch")
                            || table.LogicalName.StartsWith("codek")
                            || table.LogicalName.StartsWith("com")
                            || table.LogicalName.StartsWith("conn")
                            || table.LogicalName.StartsWith("cont")
                            || table.LogicalName.StartsWith("conver")
                            || table.LogicalName.StartsWith("custom")
                            || table.LogicalName.StartsWith("data")
                            || table.LogicalName.StartsWith("dependency")
                            || table.LogicalName.StartsWith("desk")
                            || table.LogicalName.StartsWith("dis")
                            || table.LogicalName.StartsWith("doc")
                            || table.LogicalName.StartsWith("dup")
                            || table.LogicalName.StartsWith("dvf")
                            || table.LogicalName.StartsWith("dvt")
                            || table.LogicalName.StartsWith("dyn")
                            || table.LogicalName.StartsWith("email")
                            || table.LogicalName.StartsWith("elastic")
                            || table.LogicalName.StartsWith("enti")
                            || table.LogicalName.StartsWith("env")
                            || table.LogicalName.StartsWith("exp")
                            || table.LogicalName.StartsWith("external")
                            || table.LogicalName.StartsWith("field")
                            || table.LogicalName.StartsWith("flow")
                            || table.LogicalName.StartsWith("goal")
                            || table.LogicalName.StartsWith("hierarchy")
                            || table.LogicalName.StartsWith("import")
                            || (table.LogicalName != "incident" && table.LogicalName.StartsWith("incident"))
                            || table.LogicalName.StartsWith("inter")
                            || table.LogicalName.StartsWith("invoice")
                            || table.LogicalName.StartsWith("kb")
                            || table.LogicalName.StartsWith("know")
                            || (table.LogicalName != "lead" && table.LogicalName.StartsWith("lead"))
                            || table.LogicalName.StartsWith("list")
                            || table.LogicalName.StartsWith("mail")
                            || table.LogicalName.StartsWith("manage")
                            || table.LogicalName.StartsWith("mobile")
                            || table.LogicalName.StartsWith("ms")
                            || table.LogicalName.StartsWith("multi")
                            || table.LogicalName.StartsWith("office")
                            || table.LogicalName.StartsWith("opportunity")
                            || table.LogicalName.StartsWith("organization")
                            || table.LogicalName.StartsWith("orgins")
                            || table.LogicalName.StartsWith("owner")
                            || table.LogicalName.StartsWith("phone")
                            || table.LogicalName.StartsWith("post")
                            || table.LogicalName.StartsWith("power")
                            || table.LogicalName.StartsWith("principal")
                            || table.LogicalName.StartsWith("privilege")
                            || table.LogicalName.StartsWith("process")
                            || table.LogicalName.StartsWith("product")
                            || table.LogicalName.StartsWith("publish")
                            || (table.LogicalName != "queue" && table.LogicalName.StartsWith("queue"))
                            || table.LogicalName.StartsWith("quote")
                            || table.LogicalName.StartsWith("rating")
                            || table.LogicalName.StartsWith("rec")
                            || table.LogicalName.StartsWith("relationship")
                            || table.LogicalName.StartsWith("report")
                            || table.LogicalName.StartsWith("resource")
                            || table.LogicalName.StartsWith("retention")
                            || table.LogicalName.StartsWith("ribbon")
                            || table.LogicalName.StartsWith("role")
                            || table.LogicalName.StartsWith("roll")
                            || table.LogicalName.StartsWith("rout")
                            || table.LogicalName.StartsWith("runtime")
                            || table.LogicalName.StartsWith("sales")
                            || table.LogicalName.StartsWith("save")
                            || table.LogicalName.StartsWith("sdk")
                            || table.LogicalName.StartsWith("search")
                            || table.LogicalName.StartsWith("service")
                            || table.LogicalName.StartsWith("share")
                            || table.LogicalName.StartsWith("sla")
                            || table.LogicalName.StartsWith("social")
                            || table.LogicalName.StartsWith("solution")
                            || table.LogicalName.StartsWith("stage")
                            || table.LogicalName.StartsWith("subscription")
                            || table.LogicalName.StartsWith("synap")
                            || table.LogicalName.StartsWith("sync")
                            || table.LogicalName.StartsWith("system")
                            || table.LogicalName.StartsWith("team")
                            || table.LogicalName.StartsWith("time")
                            || table.LogicalName.StartsWith("topic")
                            || table.LogicalName.StartsWith("trace")
                            || table.LogicalName.StartsWith("trans")
                            || table.LogicalName.StartsWith("uom")
                            || table.LogicalName.StartsWith("user")
                            || table.LogicalName.StartsWith("web")
                            || table.LogicalName.StartsWith("wizard")
                            || table.LogicalName.StartsWith("work")
                            || table.LogicalName.EndsWith("fewshot")
                            || table.LogicalName.EndsWith("fiscalcalendar")
                            || table.LogicalName.EndsWith("map")
                            || table.LogicalName.EndsWith("mapping")
                            || table.LogicalName.EndsWith("setting")
                            || table.LogicalName.Contains("language")
                            || table.LogicalName.Contains("metadata")
                            || table.LogicalName.Contains("plugin")
                            
};
builder.Services.AddSingleton(options);
#if !DEBUG
builder.Services.AddServerSideBlazor().AddMicrosoftIdentityConsentHandler();
#endif
var app = builder.Build();
app.UseDynamicsExplorer();
app.Run();

record Teste(int Epa, int Opa);