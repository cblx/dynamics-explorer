using Cblx.Dynamics.Explorer.Pages;
using System.Reflection.Metadata;
using System.Xml.Linq;

namespace Cblx.Dynamics.Explorer.Models;

public class DynamicsComponentFactory
{
    private readonly Dictionary<string, TableInfo> _tables = new();
    private readonly Dictionary<string, TableOptions> _tableOptions;
    private Dictionary<string, string>? _endpoints = null;

    public DynamicsComponentFactory(DynamicsExplorerOptions options)
    {
        _tableOptions = options.Tables.ToDictionary(t => t.Name);
    }

    public TableInfo CreateTable(XElement entityElement)
    {
        var name = entityElement.Attribute("Name")!.Value;
        if(_tables.ContainsKey(name)) { return _tables[name]; }
        var table = new TableInfo();
        _tables[name] = table;
        table.Initialize(entityElement, this);
        return table;
    }

    internal string? FindEndpoint(XElement tableElement)
    {
        _endpoints ??= tableElement.Document!
                   .Descendants()
                   .Where(el => el.Name.LocalName == "EntitySet")
                   //.Where(el => el.Attribute("EntityType")!.Value.EndsWith($".{tbl.TableName}"))
                   .Select(el => new
                   {
                       EndpointName = el.Attribute("Name")!.Value,
                       EntityName = el.Attribute("EntityType")!.Value.Split('.').Last()
                   }).ToDictionary(el => el.EntityName, el => el.EndpointName);
        var name = tableElement.Attribute("Name")!.Value;
        return _endpoints.TryGetValue(name, out var endpoint) ? endpoint : null;
    }

    private TableOptions? GetTableOptions(string tableName)
    {
        return _tableOptions.TryGetValue(tableName, out var options) ? options : null;
    }

    public TableOptions? GetTableOptions(XElement tableElement)
        => GetTableOptions(tableElement.Attribute("Name")!.Value);

    internal TableInfo? FindTableByOriginalName(string foreignTableName)
    {
        if(_tables.ContainsKey(foreignTableName)) { return _tables[foreignTableName]; }
        return null;
    }
}

public class TableInfo
{
    internal TableInfo() { }

    internal void Initialize(XElement entityElement, DynamicsComponentFactory factory)
    {
        DisplayName = entityElement.Attribute("Name")!.Value;
        OriginalName = DisplayName;
        var options = factory.GetTableOptions(entityElement);
        Endpoint = factory.FindEndpoint(entityElement);
        Columns = entityElement
            .Descendants()
            .Where(e => e.Name.LocalName == "Property")
            .Select(propertyElement 
                => new ColumnInfo(
                    this,
                    propertyElement, 
                    factory
                    //options?.Columns.FirstOrDefault(c => c.Name == propertyElement.Attribute("Name")!.Value)
                )
            )
            .ToList();
        Columns = Columns
            .OrderByDescending(c => c.IsPrimaryKey)
            .ThenBy(c => c.OriginalName == c.DisplayName)
            .ThenBy(c => c.OriginalName.StartsWith('_'))
            .ThenBy(c => c.DisplayName).ToList();
        if(options != null)
        {
            DisplayName = options.FriendlyName;
            OriginalName = options.Name;
        }
    }
    public string? PrimparyKeyName => Columns.Find(c => c.IsPrimaryKey)?.OriginalName;
    public string DisplayName { get; private set; } = default!;
    public string OriginalName { get; private set; } = default!;
    public string? Endpoint { get; private set; } = default!;
    public List<ColumnInfo> Columns { get; private set; } = default!;    
    public bool HasFriendlyName => DisplayName != OriginalName;
    public bool HasEndpoint => Endpoint != null;
}