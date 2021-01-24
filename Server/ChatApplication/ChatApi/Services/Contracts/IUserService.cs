using ChatApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApi.Services.Contracts
{
    public interface IUserService
    {
        User Authenticate(string email);
        IEnumerable<User> GetAll();
        User GetById(int id);
        User Create(User user);
        void Update(User user);
        void Delete(int id);
    }
}
