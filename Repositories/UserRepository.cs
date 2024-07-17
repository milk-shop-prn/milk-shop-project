using BusinessObjects.Models;
using DataAccessObjects;
using System.Collections.Generic;

namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        public User CheckLogin(string email, string password, string key)
            => UserDAO.Instance.CheckLogin(email, password,key);

        public void DeleteUser(User user)
            => UserDAO.Instance.DeleteUser(user);

        public List<User> GetAll()
            => UserDAO.Instance.GetAll();

        public User GetUserById(int id)
            => UserDAO.Instance.GetUserById(id);

        public void SaveUser(User user)
            => UserDAO.Instance.SaveUser(user);

        public bool UpdateUser(User user)
            => UserDAO.Instance.UpdateUser(user);
    }
}
