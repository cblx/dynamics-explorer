using Cblx.Dynamics.Explorer.Models;

namespace Cblx.Dynamics.Explorer;

public class DynamicsExplorerOptions
{
    public required TableOptions[] Tables { get; init; }
}