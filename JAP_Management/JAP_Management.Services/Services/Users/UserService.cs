using JAP_Management.Core.Entities;
using JAP_Management.Core.Helpers;
using JAP_Management.Core.Models;
using JAP_Management.Repositories.Repositories.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAP_Management.Services.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public async Task<string> LoginAndGenerateJWTAsync(LoginModel model)
        {
            try
            {
                var loggedUser = await LoginAsync(model);

                //converted to minutes in GetToken function
                var expirationTime = 20;

                if (loggedUser == null)
                {
                    Console.WriteLine("User not logged in");

                    return null;
                }

                var token = JwtHelper.GetToken(loggedUser, expirationTime);

                Console.WriteLine("User logged in");

                return token;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error->", ex);
                return null;
            }
        }

        private async Task<BaseUser> LoginAsync(LoginModel model)
        {
            var usersWithSameUsername = await _userRepository.GetUsersByUsernameAsync(model.Username);

            //check if user exists
            if (!usersWithSameUsername.Any())
                return null;

            foreach (var user in usersWithSameUsername)
            {
                var hahsedPassword = PasswordHashSaltGenerator.HashPassword(user.PasswordSalt, model.Password);
                if (user.PasswordHash.CompareTo(hahsedPassword) == 0)
                    return user;
            }

            return null;
        }
    }
}
