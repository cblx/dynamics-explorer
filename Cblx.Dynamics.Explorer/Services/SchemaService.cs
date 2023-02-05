using System.Xml.Linq;

namespace Cblx.Dynamics.Explorer.Services;

public class SchemaService
{
    private readonly HttpClient _httpClient;

    public Task<XDocument> DocumentTask { get; }
    public SchemaService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        DocumentTask = LoadSchemaAsync();
    }

    private async Task<XDocument> LoadSchemaAsync()
    {
        var schema = await _httpClient.GetStringAsync("$metadata");
        var xDocument = XDocument.Parse(schema);
        return xDocument;
    }
}
