using Cblx.Dynamics.Explorer.Models;
using MudBlazor;
using System.Net.Http.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.Json.Nodes;
using System.Diagnostics.Eventing.Reader;

namespace Cblx.Dynamics.Explorer.Shared;

internal class EditDialogService
{
    private readonly HttpClient _httpClient;

    public EditDialogService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task PatchAsync(Guid id, TableInfo table, EditDialogSet[] sets)
    {
        var fields = new Dictionary<string, object?>();
        foreach (var set in sets.Where(s => s.IsDirty()))
        {
            if (set.Column!.IsForeignKey)
            {
                if (set.Value is null)
                {
                    fields[$"{set.Column!.NavigationName}@odata.bind"] = null;
                }
                else
                {
                    fields[$"{set.Column!.NavigationName}@odata.bind"] = $"{set.Column.ForeignTable!.Endpoint}({set.Value})";
                }
            }
            else
            {
                fields[set.Column!.OriginalName] = set.Value;
            }
        }
        if (!fields.Any()) { return; }
        var response = await _httpClient.PatchAsJsonAsync($"{table.Endpoint}({id})", fields);
        if (!response.IsSuccessStatusCode)
        {
            var errorJson = await response.Content.ReadFromJsonAsync<JsonObject>();
            throw new InvalidOperationException(errorJson!["error"]!["message"]!.ToString());
        }
    }

    public async Task PostAsync(TableInfo table, EditDialogSet[] sets)
    {
        var fields = new Dictionary<string, object?>();
        foreach(var set in sets)
        {
            if (set.Column!.IsForeignKey)
            {
                if (set.Value is null)
                {
                    fields[$"{set.Column!.NavigationName}@odata.bind"] = null;
                }
                else
                {
                    fields[$"{set.Column!.NavigationName}@odata.bind"] = $"{set.Column.ForeignTable!.Endpoint}({set.Value})";
                }
            }
            else
            {
                fields[set.Column!.OriginalName] = set.Value;
            }
        }
        if (!fields.Any()) { return; }
        var response = await _httpClient.PostAsJsonAsync(table.Endpoint, fields);
        if (!response.IsSuccessStatusCode)
        {
            var errorJson = await response.Content.ReadFromJsonAsync<JsonObject>();
            throw new InvalidOperationException(errorJson!["error"]!["message"]!.ToString());
        }
    }
}
