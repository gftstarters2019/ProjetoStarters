using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Infrastructure.Services.Contracts
{
    public interface IEmailService
    {
        void SendEmail(string subject, string body, string to);
    }
}
