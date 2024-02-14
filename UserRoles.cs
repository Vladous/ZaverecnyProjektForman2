using System.Security.Claims;

namespace ZaverecnyProjektForman2
{
    /// <summary>
    /// Definuje konstanty pro názvy uživatelských rolí.
    /// </summary>
    public static class UserRoles
    {
        public const string Admin = "admin";
        public const string Manager = "manager";
        public const string Viewer = "viewer";
    }
    /// <summary>
    /// Poskytuje metody rozšíření pro práci s hierarchií rolí.
    /// </summary>
    public static class RoleExtensions
    {
        /// <summary>
        /// Definuje hierarchii rolí, kde vyšší čísla označují vyšší úroveň přístupových práv.
        /// </summary>
        public static readonly Dictionary<string, int> RoleHierarchy = new Dictionary<string, int>
    {
        { UserRoles.Viewer, 1 },
        { UserRoles.Manager, 2 },
        { UserRoles.Admin, 3 }
    };
        /// <summary>
        /// Určuje, zda má uživatel zadanou roli nebo roli s vyšší úrovní v hierarchii.
        /// </summary>
        /// <param name="user">Objekt ClaimsPrincipal reprezentující uživatele.</param>
        /// <param name="role">Název role pro ověření.</param>
        /// <returns>True, pokud uživatel má zadanou roli nebo vyšší. False v opačném případě.</returns>
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
