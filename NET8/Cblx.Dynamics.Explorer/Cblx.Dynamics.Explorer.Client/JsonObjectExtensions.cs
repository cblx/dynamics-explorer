using Cblx.Dynamics.Explorer.Models;
using System.Text.Json.Nodes;

namespace Cblx.Dynamics.Explorer;

internal static class JsonObjectExtensions
{
    public static bool HasError(this JsonObject? jsonObject)
        => jsonObject?.ContainsKey("error") is true;

    public static string? GetErrorMessage(this JsonObject? jsonObject)
        => jsonObject?["error"]?["message"]?.GetValue<string>();

    public static PicklistOption[] ToPicklistOptions(this JsonObject? jsonObject)
        => jsonObject is null ? Array.Empty<PicklistOption>() : jsonObject["OptionSet"]!["Options"]!
        .AsArray()
        .Select(j => new PicklistOption
        {
            Text = j!["Label"]!["LocalizedLabels"]![0]!["Label"]!.GetValue<string>(),
            Value = j["Value"]!.GetValue<int>()
        }).ToArray();
}