 using JAP_Management.Core.Entities;
using JAP_Management.Infrastructure.Database;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JAP_Management.Core.Helpers;

namespace JAP_Management.Repositories.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _databaseContext;

        public UserRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<List<BaseUser>> GetUsersByUsernameAsync(string username)
        {
            return await _databaseContext.Users.Where(u => u.UserName.CompareTo(username) == 0).ToListAsync();
        }

        public async Task<BaseUser> AddUserAsync(string firstName, string lastName,string email)
        {
            var salt = PasswordHashSaltGenerator.GenerateSalt();
            var usernamePassword = firstName+"-"+lastName;
            var newUser = new BaseUser()
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                UserName = usernamePassword,
                PasswordSalt = salt,
                PasswordHash = PasswordHashSaltGenerator.HashPassword(salt, usernamePassword)
            };
            await _databaseContext.Users.AddAsync(newUser);
            await _databaseContext.SaveChangesAsync();

            return newUser;
        }
    }
}
