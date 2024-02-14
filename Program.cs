using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ZaverecnyProjektForman2.Data;
using ZaverecnyProjektForman2.Models;

namespace ZaverecnyProjektForman2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            // Konfigurace identity.
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.User.RequireUniqueEmail = true;
                
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
            builder.Services.AddControllersWithViews();

            var app = builder.Build();
            // Vytvoøení nového scope pro vytvoøení rolí
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    // Vytvoøení rolí
                    CreateRoles(services).GetAwaiter().GetResult();
                    // Inicializace administrátorského úètu
                    ApplicationDbInitializer.EnsureAdminUser(services).GetAwaiter().GetResult();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "Chyba pøi vytváøení rolí.");
                }
            }
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.Run();
        }
        /// <summary>
        /// Vytváøí role uživatelù, pokud ještì neexistují, a inicializuje administrátorský úèet.
        /// </summary>
        /// <param name="serviceProvider">Poskytovatel služeb pro pøístup k službám.</param>
        private static async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] roleNames = { UserRoles.Admin, UserRoles.Manager, UserRoles.Viewer };
            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
            // Zkontrolovat, zda existuje uživatel s rolí "Admin"
            var adminUsers = await userManager.GetUsersInRoleAsync(UserRoles.Admin);
            if (adminUsers.Count == 0)
            {
                // Pokud žádný admin neexistuje, vytvoøit nového admina
                var adminUser = new ApplicationUser
                {
                    UserName = "admin",
                    Email = "admin@admin.cz",
                    RegistrationDate = DateTime.Now
                };
                var createAdminResult = await userManager.CreateAsync(adminUser, "adminPassword123"); // Zmìòte na bezpeènìjší heslo
                if (createAdminResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, UserRoles.Admin);
                }
            }
        }
    }
}