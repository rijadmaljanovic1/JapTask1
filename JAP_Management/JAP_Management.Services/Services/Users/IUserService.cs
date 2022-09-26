using JAP_Management.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAP_Management.Services.Services.Users
{
    public interface IUserService
    {
        Task<string> LoginAndGenerateJWTAsync(LoginModel model);
    }
}
