using JAP_Management.Core.Entities;
using JAP_Management.Core.Helpers;
using JAP_Management.Core.Models;
using JAP_Management.Repositories.Repositories.Users;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<BaseUser> _userManager;
        public UserService(IUserRepository userRepository, UserManager<BaseUser> userManager)
        {
            this._userRepository = userRepository;
            _userManager = userManager;
        }
        public async Task<BaseUser> AddUserAsync(string firstName, string lastName, string email)
        {
            try
            {
                var addedUser = await _userRepository.AddUserAsync(firstName, lastName, email);


                if (addedUser == null)
                {
                    Console.WriteLine("User not added ");

                    return null;
                }

                return addedUser;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error->", ex);
                return null;
            }
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
                var roles = await _userManager.GetRolesAsync(loggedUser);
                var token = JwtHelper.GetToken(loggedUser, expirationTime, roles);

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
