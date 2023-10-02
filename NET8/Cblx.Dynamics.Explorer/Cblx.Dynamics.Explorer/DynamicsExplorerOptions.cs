using Cblx.Dynamics.Explorer.Models;
using System.Xml.Linq;

namespace Cblx.Dynamics.Explorer;

public class DynamicsExplorerOptions
{
    public Func<IgnoreTableContext, bool>? IgnoreTables { get; init; }
    public TableOptions[] Tables { get; init; } = new TableOptions[0];
}

public class IgnoreTableContext
{
    public required string LogicalName { get; init; }
}