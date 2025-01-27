﻿using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
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
            var user = db.Users
                .Where(u => u.Email.Equals(email))
                .FirstOrDefault();
            if (user != null && aesEncryption.Decrypt(user.PasswordHash, key).Equals(password))
            {
                return user;
            }
            return null!;
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


        public bool UpdateUser(User user)
        {
            try
            {
                using var db = new MilkShopContext();
                db.Entry<User>(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
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
