namespace Backend.Infrastructure.Services.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(string subject, string body, string to);
    }
}
