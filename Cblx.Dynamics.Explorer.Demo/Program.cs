using Cblx.Dynamics.Explorer;
using Cblx.Dynamics.Explorer.Demo.Mappings;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: true);
builder.Services.AddDynamicsExplorer(
    // mapping assemblies
    typeof(TbAccount).Assembly
);
var app = builder.Build();
app.UseDynamicsExplorer();
app.Run();