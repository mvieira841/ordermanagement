using Microsoft.Extensions.DependencyInjection;
using OrderManagement.Domain.Enums;

namespace OrderManagement.Infrastructure.Persistence.Context;

public static class SeedData
{
    public static async Task Initialize(IServiceProvider serviceProvider, string testUserPw)
    {
        using (var context = new ApplicationDbContext( serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
        {
            await SetUpRoles(serviceProvider, new List<UserRole> { UserRole.Administrator, UserRole.Manager });

            var adminID = await EnsureUser(serviceProvider, testUserPw, "admin@gmail.com");
            await EnsureRole(serviceProvider, adminID, UserRole.Administrator);

            var managerID = await EnsureUser(serviceProvider, testUserPw, "manager@gmail.com");
            await EnsureRole(serviceProvider, managerID, UserRole.Manager);
        }
    }

    private static async Task SetUpRoles(IServiceProvider serviceProvider, List<UserRole> roles)
    {
        var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role.GetDescription()))
            {
                IdentityResult identityResult = await roleManager.CreateAsync(new IdentityRole(role.GetDescription()));
            }
        }
    }

    private static async Task<string> EnsureUser(IServiceProvider serviceProvider,
                                                string testUserPw, string UserName)
    {
        var userManager = serviceProvider.GetService<UserManager<User>>();

        var user = await userManager.FindByNameAsync(UserName);
        if (user == null)
        {
            user = new User
            {
                UserName = UserName,
                Email = UserName,
                EmailConfirmed = true
            };
            await userManager.CreateAsync(user, testUserPw);
        }

        if (user == null)
        {
            throw new Exception("The password is probably not strong enough!");
        }

        return user.Id;
    }

    private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider, string uid, UserRole role)
    {
        var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

        if (roleManager == null)
        {
            throw new Exception("roleManager null");
        }

        IdentityResult identityResult;
        if (!await roleManager.RoleExistsAsync(role.GetDescription()))
        {
            identityResult = await roleManager.CreateAsync(new IdentityRole(role.GetDescription()));
        }

        var userManager = serviceProvider.GetService<UserManager<User>>();

        //if (userManager == null)
        //{
        //    throw new Exception("userManager is null");
        //}

        var user = await userManager.FindByIdAsync(uid);

        if (user == null)
        {
            throw new Exception("The testUserPw password was probably not strong enough!");
        }

        identityResult = await userManager.AddToRoleAsync(user, role.GetDescription());

        return identityResult;
    }
}