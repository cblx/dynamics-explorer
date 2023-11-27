
using Cblx.Dynamics.Explorer.Services.Authenticator;

namespace Cblx.Dynamics.Explorer.Services;

public class UserContext(IHttpContextAccessor httpContextAccessor, DynamicsConfig dynamicsConfig)
{
    public string? Login => httpContextAccessor.HttpContext?.User.Identity?.Name;

    internal void AssertCanReadCurrentInstance()
    {
        var access = dynamicsConfig.Users.Find(u => u.Login == Login)?.Access ?? dynamicsConfig.DefaultAccess;
        var canRead = access is DynamicsAccess.Write or DynamicsAccess.Read;
        if (!canRead)
        {
            throw new UnauthorizedAccessException();
        }
    }

    internal void AssertCanWriteCurrentInstance()
    {
        var access = dynamicsConfig.Users.Find(u => u.Login == Login)?.Access ?? dynamicsConfig.DefaultAccess;
        var canWrite = access == DynamicsAccess.Write;
        if (!canWrite)
        {
            throw new UnauthorizedAccessException();
        }
    }
}
