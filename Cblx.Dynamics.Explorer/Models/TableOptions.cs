namespace Cblx.Dynamics.Explorer.Models;
public class TableOptions
{
    public required string FriendlyName { get; init; }
    public required string Name { get; init; }
    public List<ColumnOptions> Columns { get; init; } = new();
}
