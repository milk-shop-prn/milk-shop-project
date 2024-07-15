using BusinessObjects.Models;
using System.Collections.Generic;

namespace Services
{
    public interface IUserService
    {
        User CheckLogin(string email, string password,string key);

        List<User> GetAll();

        User GetUserById(int id);

        void SaveUser(User user);

        void UpdateUser(User user);

        void DeleteUser(User user);
    }
}
