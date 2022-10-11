using JAP_Management.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAP_Management.Repositories.Repositories.Users
{
    public interface IUserRepository
    {
        Task<List<BaseUser>> GetUsersByUsernameAsync(string username);
        Task<BaseUser> AddUserAsync(string firstName, string lastName, string email);
    }
}
