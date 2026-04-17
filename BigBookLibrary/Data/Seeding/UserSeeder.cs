using Microsoft.AspNetCore.Identity;

namespace BigBookLibrary.Data.Seeding
{
    public static class UserSeeder
    {
        public static async Task SeedUsersAsync(UserManager<IdentityUser> userManager)
        {
            var users = new List<(string Email, string Password)>
            {
                ("user1@bigbooklibrary.com", "User123!"),
                ("user2@bigbooklibrary.com", "User123!")
            };

            foreach (var (email, password) in users)
            {
                var user = await userManager.FindByEmailAsync(email);

                if (user == null)
                {
                    user = new IdentityUser
                    {
                        UserName = email,
                        Email = email,
                        EmailConfirmed = true
                    };

                    await userManager.CreateAsync(user, password);
                }

                if (!await userManager.IsInRoleAsync(user, "User"))
                {
                    await userManager.AddToRoleAsync(user, "User");
                }
            }
        }
    }
}
