using Cblx.Dynamics.Explorer.Client.Services.DynamicsServices;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddMudServices();

var services = builder.Services;
services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
services.AddDynamicsServices();

await builder.Build().RunAsync();
