using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using ShortUrl.Models;

namespace ShortUrl.Data
{
    public class UserRepository : IUserRepository
    {
        private List<User> _users = new List<User>();
        private readonly string _filePath;
        public UserRepository()
        {
            _filePath = GetFilePath();
            LoadUsers();
        }

        private string GetFilePath()
        {
            string basePath = Environment.GetEnvironmentVariable("IS_DOCKER") == "true" ? "SharedFolder" : "";
            return Path.Combine(basePath, "users.json");
        }

        public void AddUser(User user)
        {
            // Verifica si ya existe el usuario en la lista antes de agregarlo
            if (!_users.Any(u => u.UserName == user.UserName))
            {
                _users.Add(user);
                SaveChanges();
            }
        }

        public List<User> GetUsers()
        {
            return _users;
        }

        public IEnumerable<User> GetAllUsers(UserQueryParameters? userQueryParameters) {
        var query = _users.AsQueryable();


#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            if (!string.IsNullOrWhiteSpace(userQueryParameters.UserName)) 
        {
            query = query.Where(u => u.UserName.Contains(userQueryParameters.UserName));
        }
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

            var result = query.ToList();
        return result;
    }

        public User? GetUserByUserName(string userName)
        {
            return _users.FirstOrDefault(u => u.UserName == userName); 
        }

        public User? GetUserByEmail(string email)
        {
            return _users.FirstOrDefault(u => u.Email == email);
        }

        public User? GetUserById(int? userId)
        {
            return _users.FirstOrDefault(u => u.Id == userId);
        }

        public void UpdateUser(User user)
        {
            AddUser(user);
        }

        public void DeleteUser(int userId)
        {
            if (userId != 0)
            {
                var user = _users.FirstOrDefault(u => u.Id == userId);
                if (user != null)
                {
                    _users.Remove(user);
                    SaveChanges();
                }
            }
        }

        public void SaveChanges()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(_users, options);
            File.WriteAllText(_filePath, jsonString);
        }

        private void LoadUsers()
        {
            if (File.Exists(_filePath))
            {
                string jsonString = File.ReadAllText(_filePath);
                var users = JsonSerializer.Deserialize<List<User>>(jsonString);
                _users = users ?? new List<User>();

                   if (_users.Any()) 
                {
                    int maxId = _users.Max(u => u.Id);
                    User.UpdateNextUserId(maxId + 1); 
                }  
            }
        }
    }
}
