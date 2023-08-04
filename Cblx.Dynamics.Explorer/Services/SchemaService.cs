using System.Xml.Linq;
using Cblx.Dynamics.Explorer.Models;

namespace Cblx.Dynamics.Explorer.Services;

internal class SchemaService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly DynamicsExplorerOptions _options;
    private readonly DynamicsComponentFactory _factory;
    private TableInfo[] _tables  = Array.Empty<TableInfo>();

    public Task<XDocument> DocumentTask { get; }
    public SchemaService(
        //HttpClient httpClient, 
        IHttpClientFactory httpClientFactory,
        DynamicsExplorerOptions options, DynamicsComponentFactory factory)
    {
        _httpClientFactory = httpClientFactory;
        _options = options;
        _factory = factory;
        DocumentTask = LoadSchemaAsync();
    }

    private async Task<XDocument> LoadSchemaAsync()
    {
        var httpClient = _httpClientFactory.CreateClient("IODataClient");
        var schema = await httpClient.GetStringAsync("$metadata");
        var xDocument = XDocument.Parse(schema);
        return xDocument;
    }

    internal async Task<TableInfo[]> GetTablesAsync()
    {
        if(_tables.Any()) { return _tables; }
        var document = await DocumentTask;
        var tables = document
            .Descendants()
            .Where(e => e.Name.LocalName == "EntityType")
            .Where(e => _options.IgnoreTables == null 
                    || !_options.IgnoreTables(new IgnoreTableContext { LogicalName = e.Attribute("Name")!.Value }))
            .Select(e => _factory.CreateTable(e))
            .Where(t => t.HasEndpoint)
            .OrderBy(t => t.OriginalName == t.DisplayName)
            .ToArray();
        _tables = tables;
        Console.WriteLine($"Loaded {_tables.Length} tables");
        return tables;
    }
}
