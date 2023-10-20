using Cblx.Dynamics.Explorer.Client.Components;

namespace Cblx.Dynamics.Explorer.Client.Services;

public class InstanceContextService
{
    public string? Group { get; set; }
    public string? Instance { get; set; }

    public string GetRoute(string path)
    {
        return $"{path}?group={Group}&instance={Instance}";
    }
}
