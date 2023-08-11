using System.Text.Json.Nodes;

namespace Cblx.Dynamics.Explorer;

public static class JsonObjectExtensions
{
    public static bool HasError(this JsonObject? jsonObject)
        => jsonObject?.ContainsKey("erro") is true;

    public static string? GetErrorMessage(this JsonObject? jsonObject)
        => jsonObject?["error"]?["message"]?.GetValue<string>();
}