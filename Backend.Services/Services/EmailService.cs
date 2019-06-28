using Backend.Infrastructure.Services.Interfaces;
using System.Net;
using System.Net.Mail;

namespace Backend.Services.Services
{
    public class EmailService : IEmailService
    {
        public void SendEmail(string emailSubject, string emailBody, string destinationEmailAddress)
        {
            var fromAddress = new MailAddress("gftstarter@gmail.com", "GFT Starter");
            var toAddress = new MailAddress(destinationEmailAddress, "Contract Holder");

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, "Gft@2019@1")
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = emailSubject,
                Body = emailBody
            })
            {
                smtp.Send(message);
            }
        }
    }
}
