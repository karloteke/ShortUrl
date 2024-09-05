using System;
using System.Collections.Generic;
using System.Linq;
using ShortUrl.Models;
using Microsoft.EntityFrameworkCore;

namespace ShortUrl.Data
{
    public class UserEFRepository : IUserRepository
    {
        private readonly ShortUrlContext _context;

        public UserEFRepository(ShortUrlContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public IEnumerable<User> GetAllUsers(UserQueryParameters? userQueryParameters)
        {
            IQueryable<User> query = _context.Users;

            if (userQueryParameters != null && !string.IsNullOrWhiteSpace(userQueryParameters.UserName))
            {
                query = query.Where(u => u.UserName.Contains(userQueryParameters.UserName));
            }

            return query.ToList();
        }

        public User? GetUserByUserName(string userName)
        {
            return _context.Users.FirstOrDefault(u => u.UserName == userName);
        }

        public List<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public User? GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public User? GetUserById(int? userId)
        {
            return userId.HasValue ? _context.Users.FirstOrDefault(u => u.Id == userId) : null;
        }

        public void UpdateUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteUser(int userId)
        {
            var userToDelete = GetUserById(userId);
            if (userToDelete != null)
            {
                _context.Users.Remove(userToDelete);
                _context.SaveChanges();
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

    }
}
