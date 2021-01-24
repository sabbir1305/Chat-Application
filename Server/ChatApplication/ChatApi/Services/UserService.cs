using ChatApi.Data;
using ChatApi.Entities;
using ChatApi.Helpers;
using ChatApi.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApi.Services
{
    public class UserService : IUserService
    {
        private DataContext _context;

        //Inject Db Context
        public UserService(DataContext context)
        {
            _context = context;
        }

        public User Authenticate(string email)
        {
            if (string.IsNullOrEmpty(email) )
                return null;

            var user = _context.Users.SingleOrDefault(x => x.Email == email);

            // check if username exists
            if (user == null)
                return null;

            // authentication successful
            return user;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public User GetById(int id)
        {
            return _context.Users.Find(id);
        }

        public User Create(User user)
        {
            // validation
            if (string.IsNullOrWhiteSpace(user.Email))
                throw new AppException("Email is required");

            if (_context.Users.Any(x => x.Email == user.Email))
                throw new AppException("Email \"" + user.Email + "\" is already taken");

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        public void Update(User userParam)
        {
            var user = _context.Users.Find(userParam.Id);

            if (user == null)
                throw new AppException("User not found");

            // update username if it has changed
            if (!string.IsNullOrWhiteSpace(userParam.Email) && userParam.Email != user.Email)
            {
                // throw error if the new username is already taken
                if (_context.Users.Any(x => x.Email == userParam.Email))
                    throw new AppException("Email " + userParam.Email + " is already taken");

                user.Email = userParam.Email;
            }

            // update user properties if provided
            if (!string.IsNullOrWhiteSpace(userParam.FirstName))
                user.FirstName = userParam.FirstName;

            if (!string.IsNullOrWhiteSpace(userParam.LastName))
                user.LastName = userParam.LastName;


            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }
    }
}
