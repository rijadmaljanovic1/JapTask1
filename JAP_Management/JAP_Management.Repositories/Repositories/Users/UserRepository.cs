using JAP_Management.Core.Entities;
using JAP_Management.Infrastructure.Database;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return await _databaseContext.Users.Where(u => u.Username.CompareTo(username) == 0).ToListAsync();
        }
    }
}
