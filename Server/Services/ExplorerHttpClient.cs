using Microsoft.Extensions.DependencyInjection;

namespace Cblx.Dynamics.Explorer.Services;

public class ExplorerHttpClient([FromKeyedServices("dynamics.explorer")]HttpClient httpClient)
{
    public HttpClient HttpClient => httpClient;
}
