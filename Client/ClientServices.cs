using Cblx.Dynamics.Explorer.Client.Services;
using Cblx.Dynamics.Explorer.Client.Services.DynamicsServices;

namespace Cblx.Dynamics.Explorer.Client;

public static class ClientServices
{
    public static IServiceCollection AddClientServices(this IServiceCollection services, string baseAddress)
    {
        services.AddFluentUIComponents();
        services.AddSingleton<InstanceContextService>();
        services.AddSingleton(sp => {
            var instanceService = sp.GetRequiredService<InstanceContextService>();
            var httpClient = new HttpClient(new InstanceScopeHandler(instanceService) { InnerHandler = new HttpClientHandler() }) { 
                BaseAddress = new Uri(baseAddress) 
            };
            return httpClient;
        });
        
        services.AddDynamicsServices();
        return services;
    }

    class InstanceScopeHandler(InstanceContextService instanceContextService) : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("x-Dynamics-Explorer-Group", instanceContextService.Group);
            request.Headers.Add("x-Dynamics-Explorer-Instance", instanceContextService.Instance);
            return base.SendAsync(request, cancellationToken);
        }
    }
}
