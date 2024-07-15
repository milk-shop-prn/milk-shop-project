using BusinessObjects.Models;
using Repositories;
using System.Collections.Generic;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService()
        {
            this.userRepository = new UserRepository();
        }

        public User CheckLogin(string email, string password,string key)
        {
            return this.userRepository.CheckLogin(email, password,key);
        }

        public void DeleteUser(User user)
        {
            this.userRepository.DeleteUser(user);
        }

        public List<User> GetAll()
        {
            return this.userRepository.GetAll();
        }

        public User GetUserById(int id)
        {
            return this.userRepository.GetUserById(id);
        }

        public void SaveUser(User user)
        {
            this.userRepository.SaveUser(user);
        }

        public void UpdateUser(User user)
        {
            this.userRepository.UpdateUser(user);
        }
    }
}
