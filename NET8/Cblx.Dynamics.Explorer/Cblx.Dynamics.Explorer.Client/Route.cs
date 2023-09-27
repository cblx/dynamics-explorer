namespace Cblx.Dynamics.Explorer.Client;

public static class Route
{
    public static string GetEndpoint<T>()
    {
        return typeof(T).FullName!;
    }
}
