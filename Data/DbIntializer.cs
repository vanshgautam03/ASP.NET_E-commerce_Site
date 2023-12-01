using Microsoft.AspNetCore.Identity;

namespace project.Models
{
    public class DbInitializer
    {
        public static async Task Initialize(
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager)
        {
            string[] roleNames = { "Admin", "Customer", "DepartmentAdmin" };
            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                    await roleManager.CreateAsync(new IdentityRole(roleName));
            }

            // Create an Admin user
            var user = new IdentityUser
            {
                UserName = "admin@admin.com",
                Email = "admin@admin.com",
            };

            string userPWD = "Password@123";
            var createUser = await userManager.CreateAsync(user, userPWD);

            if (createUser.Succeeded)
                await userManager.AddToRoleAsync(user, "Admin");
        }
    }
}