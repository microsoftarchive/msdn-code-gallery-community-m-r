using Microsoft.AspNetCore.Identity;

namespace Auth.DataAccess
{
    public static class UserData
    {
        private static readonly string[] emails = {
            "Alice.Brown@example.com",
            "Ann.Blare@example.com",
            "Cathy.Jones@example.com",
            "John.Miller@example.com"
        };

        private static IdentityUser[] GetUsers()
        {
            var users = new IdentityUser[emails.Length];

            for (var i = 0; i < emails.Length; i++)
            {
                users[i] = new IdentityUser
                {
                    UserName = emails[i],
                    Email = emails[i]
                };
            }

            return users;
        }

        public static IdentityUser[] Users { get; } = GetUsers();

        public static string[] Passwords { get; } = 
        {
            "AliceBrown1234#", "AnnBlare1234#", "CathyJones1234#", "JohnMiller1234#"
        };
    }
}
