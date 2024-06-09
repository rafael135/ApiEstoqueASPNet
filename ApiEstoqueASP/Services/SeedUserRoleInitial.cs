using ApiEstoqueASP.Models;
using ApiEstoqueASP.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace ApiEstoqueASP.Services
{
    public class SeedUserRoleInitial : ISeedUserRoleInitial
    {

        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedUserRoleInitial(UserManager<User> userManager ,RoleManager<IdentityRole> roleManager)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        public async Task SeedRoles()
        {
            var roleUserExists = await _roleManager.RoleExistsAsync("User");

            if(roleUserExists == false)
            {
                IdentityRole role = new IdentityRole();

                role.Name = "User";
                role.NormalizedName = "USER";
                role.ConcurrencyStamp = Guid.NewGuid().ToString();

                IdentityResult roleResult = await _roleManager.CreateAsync(role);
            }

            var roleAdminExists = await _roleManager.RoleExistsAsync("Admin");

            if(roleAdminExists == false)
            {
                IdentityRole role = new IdentityRole();

                role.Name = "Admin";
                role.NormalizedName = "ADMIN";
                role.ConcurrencyStamp = Guid.NewGuid().ToString();

                IdentityResult roleResult = await _roleManager.CreateAsync(role);
            }
        }

        public async Task SeedUsers()
        {
            var initialUserExists = await _userManager.FindByEmailAsync("user@email.com");

            if(initialUserExists == null)
            {
                User user = new User();

                user.UserName = "user";
                user.Email = "user@email.com";
                user.NormalizedUserName = "USER";
                user.NormalizedEmail = "USER@EMAIL.COM";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = await _userManager.CreateAsync(user, "00000000Teste#");

                if(result.Succeeded == true)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                }
            }

            var adminUserExists = await _userManager.FindByEmailAsync("admin@email.com");

            if(adminUserExists == null)
            {
                User user = new User();

                user.UserName = "admin";
                user.Email = "admin@email.com";
                user.NormalizedUserName = "ADMIN";
                user.NormalizedEmail = "ADMIN@email.COM";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = await _userManager.CreateAsync(user, "00000000Teste#");

                if(result.Succeeded == true)
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                }
            }
        }
    }
}
