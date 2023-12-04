using Cblx.Dynamics.Explorer.Services.Authenticator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Cblx.Dynamics.Explorer.Services.DynamicsServices.Data.ExecuteSqlQuery;

public static class ExecuteSqlQueryHandler
{
    public static async IAsyncEnumerable<JsonObject> Handle([FromBody]string sql, DynamicsConfig config)
    {
        var sqlServer = config.ResourceUrl.Replace("api.", "")
                    .Replace("https://", "")
                    .Replace("/", "");
        var connectionString = $"Server={sqlServer}; Authentication=Active Directory Service Principal; Encrypt=True; User Id={config.ClientId}; Password={config.ClientSecret};";
        using var sqlConnection = new SqlConnection(connectionString);
        await sqlConnection.OpenAsync();
        using var sqlCommand = new SqlCommand(sql, sqlConnection);
        using var sqlDataReader = await sqlCommand.ExecuteReaderAsync();
        int currentSet = 0;
        do
        {
            int itemCount = 0;
            while (await sqlDataReader.ReadAsync())
            {
                var jsonObject = new JsonObject
                {
                    { "resultSet", currentSet },
                    { "isEmptyRow", false }
                };
                for (var i = 0; i < sqlDataReader.FieldCount; i++)
                {
                    var fieldName = sqlDataReader.GetName(i);
                    var fieldValue = sqlDataReader.GetValue(i);
                    jsonObject.Add(fieldName, JsonNode.Parse(JsonSerializer.Serialize(fieldValue)));
                }
                itemCount++;
                yield return jsonObject;
            }
            if (itemCount == 0)
            {
                var emptyObject = new JsonObject
                {
                    { "resultSet", currentSet },
                    { "isEmptyRow", true }
                };
                for(var i = 0; i < sqlDataReader.FieldCount; i++)
                {
                    var fieldName = sqlDataReader.GetName(i);
                    emptyObject.Add(fieldName, JsonNode.Parse(JsonSerializer.Serialize<string?>(null)));
                }
                yield return emptyObject;
            }
            currentSet++;
        } while (await sqlDataReader.NextResultAsync());
    }
}
