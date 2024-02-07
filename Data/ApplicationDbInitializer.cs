using Microsoft.AspNetCore.Identity;
using ZaverecnyProjektForman2.Models;

namespace ZaverecnyProjektForman2.Data
{
    public static class ApplicationDbInitializer
    {
        public static async Task EnsureAdminUser(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Zkontrolujte, zda role 'admin' existuje, pokud ne, vytvořte ji
            if (!await roleManager.RoleExistsAsync("admin"))
            {
                var role = new IdentityRole("admin");
                await roleManager.CreateAsync(role);
            }

            // Zjistěte, zda existuje jakýkoli uživatel s rolí 'admin'
            var adminUsers = await userManager.GetUsersInRoleAsync("admin");
            if (adminUsers == null || adminUsers.Count == 0)
            {
                var adminUser = new ApplicationUser { UserName = "admin", Email = "admin@admin.cz" };
                await userManager.CreateAsync(adminUser, "admin"); // Použijte silné heslo
                await userManager.AddToRoleAsync(adminUser, "admin");
            }
        }
    }

}
