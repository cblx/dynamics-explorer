using Cblx.Dynamics.Explorer.Client;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddClientServices(builder.HostEnvironment.BaseAddress);
await builder.Build().RunAsync();
