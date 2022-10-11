using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAP_Management.Services.Services.EmailSender
{
    public class EmailService : IEmailService
    {
        public async Task SendMail(string email, string usernamePassword)
        {
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("rijadmaljanovic98@gmail.com", "Example User");
            var subject = "Your login credentials for JAP Management";
            var to = new EmailAddress(email, usernamePassword);
            var plainTextContent = "Now you can login to the application.";
            var htmlContent = "<strong> Your username: "+ usernamePassword + " ; Password: "+ usernamePassword + ". Login with the credentials.</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
