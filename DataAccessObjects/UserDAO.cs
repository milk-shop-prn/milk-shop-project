using BusinessObjects.Models;
using Microsoft.Extensions.Configuration;
using MilkShop.config;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessObjects
{
    public class UserDAO
    {
        private static UserDAO instance = null!;
        private static readonly object lockObject = new object();

        private UserDAO() { }

        public static UserDAO Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new UserDAO();
                    }
                    return instance;
                }
            }
        }
        
        public User CheckLogin(string email, string password, string key)
        {
            AesEncryption aesEncryption = new AesEncryption();
            using var db = new MilkShopContext();
            db.Users.ToList().Where(c => c.Email.Equals(email) && aesEncryption.Decrypt(c.PasswordHash,key).Equals(password)).First();
            return db.Users.SingleOrDefault(u => u.Email.Equals(email) && u.PasswordHash.Equals(password));
        }

        public List<User> GetAll()
        {
            using var db = new MilkShopContext();
            return db.Users.ToList();
        }

        public User GetUserById(int id)
        {
            using var db = new MilkShopContext();
            return db.Users.FirstOrDefault(u => u.UserId.Equals(id));
        }

        public void SaveUser(User user)
        {
            try
            {
                using var db = new MilkShopContext();
                db.Users.Add(user);
                db.SaveChanges();
                
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

        public void UpdateUser(User user)
        {
            try
            {
                using var db = new MilkShopContext();
                db.Entry<User>(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteUser(User user)
        {
            try
            {
                using var db = new MilkShopContext();
                var u = db.Users.SingleOrDefault(u => u.UserId == user.UserId);
                if (u != null)
                {
                    db.Users.Remove(u);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
