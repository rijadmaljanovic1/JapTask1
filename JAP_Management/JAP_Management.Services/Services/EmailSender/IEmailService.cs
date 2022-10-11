using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAP_Management.Services.Services.EmailSender
{
    public interface IEmailService
    {
        Task SendMail(string email, string usernamePassword);

    }
}
