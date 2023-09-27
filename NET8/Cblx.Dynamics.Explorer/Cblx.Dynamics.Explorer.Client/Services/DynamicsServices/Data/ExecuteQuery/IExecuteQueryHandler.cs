using System.Text.Json.Nodes;

namespace Cblx.Dynamics.Explorer.Services.DynamicsServices.ExecuteQuery;

public interface IExecuteQueryHandler
{
    Task<JsonObject?> GetAsync(string query);
}
