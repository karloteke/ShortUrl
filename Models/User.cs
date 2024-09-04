namespace ShortUrl.Models
{
    public class User
    {
        private static int _nextId = 1; // Contador estático para la clase User

        public int Id { get; private set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string Role { get; set; } = Roles.User;

        public User(string userName, string password, string email)
        {
            Id = _nextId++;
            UserName = userName;
            Password = password;
            Email = email;
        }

        public static void UpdateNextUserId(int nextId)
        {
            _nextId = nextId;
        }
    }
}
