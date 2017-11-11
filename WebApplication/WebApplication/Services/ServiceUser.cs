using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using WebApplication.Contexts;

namespace WebApplication.Services
{
    public class ServiceUser
    {
        private const int saltSize = 16;
        private const int deriveIterations = 10000;
        private const int hashSize = 20;
        private const int hashAndSaltSize = 36;

        public static void CreateUser(string userName, string email, string password)
        {
            string passwordHash = GeneratePasswordHash(password);
            var user = new Models.ModelUser { UserName = userName, PasswordHash = passwordHash, Email = email };
            using (var context = new Contexts.MainContext())
            {
                try
                {
                    context.Users.Add(user);
                    context.SaveChanges();
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException e)
                {
                    throw new WebApplication.Exeptions.UserCreationExeption {ErrorMessage = "Email or username already exists." };
                }
            }
            
        }

        private static string GeneratePasswordHash(string password)
        {
            byte[] salt = new byte[saltSize];
            new RNGCryptoServiceProvider().GetBytes(salt);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, deriveIterations);
            byte[] hash = pbkdf2.GetBytes(hashSize);
            byte[] hashBytes = new byte[hashAndSaltSize];
            Array.Copy(salt, 0, hashBytes, 0, saltSize);
            Array.Copy(hash, 0, hashBytes, saltSize, hashSize);
            return Convert.ToBase64String(hashBytes);
        }
    }
}