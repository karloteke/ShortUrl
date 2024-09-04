using ShortUrl.Models;

namespace ShortUrl.Data
{
    public interface IUserRepository
    {
        void AddUser(User user);
        User? GetUserByUserName(string userName);
        List<User> GetUsers();
        public IEnumerable<User> GetAllUsers(UserQueryParameters? userQueryParameters);
        User? GetUserByEmail(string Email);
        User? GetUserById (int? UserId);
        void UpdateUser(User user);
        void DeleteUser(int userId);
        void SaveChanges();
    }
}