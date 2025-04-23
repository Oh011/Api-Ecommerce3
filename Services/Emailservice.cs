using Microsoft.Extensions.Configuration;
using Services.Abstractions;
using System.Net;
using System.Net.Mail;

namespace Services
{
    public class EmailService : IEmailService
    {


        private readonly SmtpClient _smtpClient;

        private readonly string _FromEmail;


        public EmailService(IConfiguration configuration)
        {


            var SMTPServer = configuration.GetSection("EmailSettings")["SmtpServer"];

            var PortNumber = int.Parse(configuration.GetSection("EmailSettings")["Port"]);


            this._smtpClient = new SmtpClient(SMTPServer, PortNumber);


            this._smtpClient.EnableSsl = true;

            this._smtpClient.Credentials = new NetworkCredential()
            {

                UserName = configuration.GetSection("EmailSettings")["Username"],
                Password = configuration.GetSection("EmailSettings")["Password"],


            };
        }

        public async Task SendEmail(string To, string subject, string Body)
        {


            var mailmessage = new MailMessage()
            {

                From = new MailAddress(_FromEmail),
                Subject = subject,
                Body = Body
            };

            mailmessage.To.Add(To);


            await _smtpClient.SendMailAsync(mailmessage);
        }
    }
}
