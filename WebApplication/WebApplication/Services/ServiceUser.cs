using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Web;
using WebApplication.Contexts;
using WebApplication.Exeptions;
using WebApplication.Models;

namespace WebApplication.Services
{
    public class ServiceUser
    {
        private const int SaltSize = 16;
        private const int DeriveIterations = 10000;
        private const int HashSize = 20;
        private const int HashAndSaltSize = SaltSize + HashSize;

        public static void CreateUser(string userName, string email, string password, MainContext db)
        {
            var passwordHash = GeneratePasswordHash(password);
            var user = new Models.ModelUser {UserName = userName, PasswordHash = passwordHash, Email = email};
            try
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException)
            {
                throw new WebApplication.Exeptions.UserCreationExeption
                {
                    ErrorMessage = "Email or username already exists."
                };
            }
        }

        public static ModelUser GetLoggedUser(string userName, MainContext db)
        {
            var matchingUsers = from user in db.Users where user.UserName == userName select user;
            if (!matchingUsers.Any())
            {
                return null;
            }
            if (matchingUsers.Count() > 1)
            {
                throw new InDataError();
            }
            return matchingUsers.First();
        }

        public static Models.ModelUser AuthorizeUser(string userName, string password, MainContext db)
        {
            var namedUsers = from user in db.Users
                where user.UserName == userName
                select user;
            if (!namedUsers.Any())
            {
                return null;
            }
            if (namedUsers.Count() > 1)
            {
                throw new WebApplication.Exeptions.InDataError();
            }

            return CheckPassword(password, namedUsers.First().PasswordHash) ? namedUsers.First() : null;
        }

        private static string GeneratePasswordHash(string password)
        {
            var salt = new byte[SaltSize];
            new RNGCryptoServiceProvider().GetBytes(salt);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, DeriveIterations);
            var hash = pbkdf2.GetBytes(HashSize);
            var hashAndSalt = new byte[HashAndSaltSize];
            Array.Copy(salt, 0, hashAndSalt, 0, SaltSize);
            Array.Copy(hash, 0, hashAndSalt, SaltSize, HashSize);
            return Convert.ToBase64String(hashAndSalt);
        }

        private static bool CheckPassword(string password, string hashedPassword)
        {
            var hashedPasswordAndSalt = Convert.FromBase64String(hashedPassword);
            var salt = new byte[SaltSize];
            Array.Copy(hashedPasswordAndSalt, 0, salt, 0, SaltSize);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, DeriveIterations);
            var hash = pbkdf2.GetBytes(HashAndSaltSize);
            for (var i = 0; i < HashSize; i++)
            {
                if (hashedPasswordAndSalt[i + SaltSize] != hash[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}