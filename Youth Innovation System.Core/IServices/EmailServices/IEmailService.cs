namespace Youth_Innovation_System.Core.IServices.EmailServices
{
    public interface IEmailService
    {
        //General
        Task SendEmailAsync(string to, string subject, string body);

    }
}
