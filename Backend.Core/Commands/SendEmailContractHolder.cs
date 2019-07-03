using System.Net;
using System.Net.Mail;

namespace Backend.Core.Commands
{
    public class SendEmailContractHolder
    {
        public SendEmailContractHolder (string email, string name)
        {
            Email = email;
            Name = name;

            var fromAddress = new MailAddress("gftstarter@gmail.com", "Projeto Starter");
            var toAddress = new MailAddress(Email, "Projeto Starter");

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
                Subject = $"Welcome {Name}!",
                Body = $"Welcome {Name}!"
            })
            {
                smtp.Send(message);
            }
        }


        public string Email { get; set; }
        public string Name { get; set; }
    }
}
