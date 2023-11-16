namespace Cblx.Dynamics.Explorer.Client;

public static class Routes
{
    public static string GetEndpoint<T>()
    {
        return typeof(T).FullName!;
    }
}
