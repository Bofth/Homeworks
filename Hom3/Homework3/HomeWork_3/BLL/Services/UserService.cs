using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Threading.Tasks;
using BLL.Abstractions.Interfaces;
using Core.Models;
using DAL.Abstractions.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.VisualBasic;


namespace BLL.Services
{
    public class UserService : GenericService<User>, IUserService
    {

        public UserService(IRepository<User> repository) :
            base(repository)
        {

        }

        public async Task<User> Authenticate(string username, string password)
        {
            var user = await GetByPredicate(i => i.Username == username && i.PasswordHash == password);

            if (user == null)
            {
                throw new Exception("user not found");
            }

            return user;
        }

        public async Task<User> GetUserByUsername(string username)
        {
            var user = await GetByPredicate(i => i.Username == username);
            if (user == null) {
                throw new Exception("User not found");
            }
            return user;
        }

        public async Task<List<User>> GetUsersByRole(string role)
        {
            var users = await GetAll();
            var usersByRole = users.Where(u => u.Role.ToString() == role).ToList();
            if (usersByRole == null)
            {
                throw new Exception("User not found");
            }
            return usersByRole;
        }

        public async Task UpdatePassword(Guid userId, string newPassword)
        {
            var user = await GetByPredicate(i => i.Id == userId);
            if(user == null)
            {
                throw new Exception("User not found");
            }
            user.PasswordHash = HashPassword(newPassword);
        }

        public async Task ResetPassword(Guid userId)
        {
            var user = await GetByPredicate(i => i.Id == userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            user.PasswordHash = null;
        }

        public async Task LockUser(Guid userId)
        {
            var user = await GetByPredicate(i => i.Id == userId && i.IsLocked == false);
            if (user == null)
            {
                throw new Exception("User not found or the user is already locked");
            }
            user.IsLocked = true;
        }

        public async Task UnlockUser(Guid userId)
        {
            var user = await GetByPredicate(i => i.Id == userId && i.IsLocked == true);
            if (user == null)
            {
                throw new Exception("User not found or the user is already unloked");
            }
            user.IsLocked = true;
        }
    }
}