using System.Security.Claims;

namespace ZaverecnyProjektForman2
{
    public static class UserRoles
    {
        public const string Admin = "admin";
        public const string Manager = "manager";
        public const string Viewer = "viewer";
    }
    public static class RoleExtensions
    {
        public static readonly Dictionary<string, int> RoleHierarchy = new Dictionary<string, int>
    {
        { UserRoles.Viewer, 1 },
        { UserRoles.Manager, 2 },
        { UserRoles.Admin, 3 }
    };

        public static bool IsInRoleOrHigher(this ClaimsPrincipal user, string role)
        {
            if (!RoleHierarchy.ContainsKey(role)) return false;

            var userRoleLevel = user.Claims
                .Where(c => c.Type == ClaimTypes.Role && RoleHierarchy.ContainsKey(c.Value))
                .Select(c => RoleHierarchy[c.Value])
                .DefaultIfEmpty(0)
                .Max();

            return userRoleLevel >= RoleHierarchy[role];
        }
    }
}
