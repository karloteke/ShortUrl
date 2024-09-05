namespace ShortUrl.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string Role { get; set; }  = Roles.User;

        private static int NextUserId = 1;

        // Constructor sin parámetros (necesario para EF Core)
        public User() { }

        public User(string? userName, string? password, string? email)
        { 
            // Id = NextUserId++;
            UserName = userName;
            Password = password;
            Email = email;
        }

         // Constructor con cuatro parámetros
        public User(string? userName, string? password, string? email, string role)
        {
            UserName = userName;
            Password = password;
            Email = email;
            Role = role;
        }

        public static void UpdateNextUserId(int nextId)
        {
            // NextUserId = nextId;
        }
    }
}
