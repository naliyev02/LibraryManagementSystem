using LibraryManagementSystem.Core.Entities.Identity;
using LibraryManagementSystem.DataAccess.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryManagementSystem.Business.Seed;

public static class DatabaseSeeder
{
    private static readonly string[] StartingRoles =
    {
        "Admin",
        "Librarian",
        "Publisher",
        "Author",
        "Member"
    };

    private const string AdminRole = "Admin";
    private const string AdminUserName = "admin";

    public static async Task SeedAsync(IServiceProvider services)
    {
        var dbContext = services.GetRequiredService<AppDbContext>();
        await dbContext.Database.MigrateAsync();

        var roleManager = services.GetRequiredService<RoleManager<AppRole>>();
        var userManager = services.GetRequiredService<UserManager<AppUser>>();

        foreach (var roleName in StartingRoles)
        {
            if (await roleManager.RoleExistsAsync(roleName))
                continue;

            var roleResult = await roleManager.CreateAsync(new AppRole(roleName));
            if (!roleResult.Succeeded)
                throw new InvalidOperationException(
                    $"{roleName} rolu yaradıla bilmədi: {FormatErrors(roleResult)}");
        }

        var admin = await userManager.FindByNameAsync(AdminUserName);
        if (admin is null)
        {
            admin = new AppUser
            {
                UserName = AdminUserName,
                Email = "admin@library.local",
                EmailConfirmed = true
            };

            var createResult = await userManager.CreateAsync(admin, "Admin123!");
            if (!createResult.Succeeded)
                throw new InvalidOperationException(
                    $"Admin istifadəçi yaradıla bilmədi: {FormatErrors(createResult)}");
        }

        if (!await userManager.IsInRoleAsync(admin, AdminRole))
        {
            var addRoleResult = await userManager.AddToRoleAsync(admin, AdminRole);
            if (!addRoleResult.Succeeded)
                throw new InvalidOperationException(
                    $"Admin rolu istifadəçiyə verilə bilmədi: {FormatErrors(addRoleResult)}");
        }

        if (!admin.EmailConfirmed)
        {
            admin.EmailConfirmed = true;
            var confirmResult = await userManager.UpdateAsync(admin);
            if (!confirmResult.Succeeded)
                throw new InvalidOperationException(
                    $"Admin email təsdiqi yenilənə bilmədi: {FormatErrors(confirmResult)}");
        }
    }

    private static string FormatErrors(IdentityResult result)
        => string.Join(", ", result.Errors.Select(e => e.Description));
}
