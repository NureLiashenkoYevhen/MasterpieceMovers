using Core.Entities;
using Core.Enums;

namespace API.Extensions;

public static class HttpContextExtension
{
    public static bool HasAdminRole(this HttpContext httpContext)
    {
        var user = httpContext.Items["User"] as User;
        return user?.Role == RoleEnum.Admin;
    }
}