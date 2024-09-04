using System.Collections.Generic; 
using ShortUrl.Models; 

namespace ShortUrl.Business
{
    public interface IUserService
    {
        User CreateUser(string userName, string password, string email);
        User Authenticate(string userName, string password);
        List<User> GetAllUsers(); 
        public IEnumerable<User> GetAllUsers(UserQueryParameters? userQueryParameters);
        User? GetUserByUserName(string userName);
        User? GetUserByEmail(string userEmail);
        User? GetUserById(int userId);
        public void UpdateUserDetails(int userId, UserUpdateDTO UserUpdate);
        public void DeleteUser(int userId);
    }
}
