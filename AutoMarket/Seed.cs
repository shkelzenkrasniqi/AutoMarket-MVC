using AutoMarket.Data;
using Microsoft.AspNetCore.Identity;

namespace AutoMarket
{
    public class Seed
    {
        public static async Task SeedData(RoleManager<IdentityRole> _roleManager, ApplicationDbContext _context)
        {
            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            if (!await _roleManager.RoleExistsAsync("User"))
            {
                await _roleManager.CreateAsync(new IdentityRole("User"));
            }
            await _context.SaveChangesAsync();

        }
    }

}