using Cblx.Dynamics;
using Cblx.Dynamics.Explorer;
using Cblx.Dynamics.Explorer.Demo.Mappings;
using Cblx.Dynamics.Explorer.Models;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: true);
builder.Services.AddDynamicsExplorer(
    // mapping assemblies
    typeof(TbAccount).Assembly
);
var options = new DynamicsExplorerOptions
{
    IgnoreTables = table => table.LogicalName.StartsWith("ms") 
                            || table.LogicalName.StartsWith("app")
                            || table.LogicalName.StartsWith("book")
                            || table.LogicalName.StartsWith("bot"),
    Tables = typeof(TbAccount).Assembly
        .GetTypes()
        .Where(t => t.GetCustomAttribute<DynamicsEntityAttribute>() != null)
        .Select(t => new TableOptions
        {
            Name = t.GetCustomAttribute<DynamicsEntityAttribute>()!.Name,
            FriendlyName = t.Name,
            Columns = t.GetProperties()
                .Where(p => p.GetCustomAttribute<JsonPropertyNameAttribute>() != null)
                .Select(p => new ColumnOptions
                {
                    Name = p.GetCustomAttribute<JsonPropertyNameAttribute>()!.Name,
                    FriendlyName = p.Name
                })
                .ToList()
        })
        .ToArray()
};
builder.Services.AddSingleton(options);
var app = builder.Build();
app.UseDynamicsExplorer();
app.Run();