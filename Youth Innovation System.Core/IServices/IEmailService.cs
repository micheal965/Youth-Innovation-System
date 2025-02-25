namespace Youth_Innovation_System.Core.IServices
{
    public interface IEmailService
    {
        //General
        Task SendEmailAsync(string to, string subject, string body);

    }
}
