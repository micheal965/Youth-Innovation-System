using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Youth_Innovation_System.Core.IServices;

namespace Youth_Innovation_System.Service
{
	public class EmailService : IEmailService
	{
		private readonly IConfiguration _configuration;

		public EmailService(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		public async Task SendEmailAsync(string to, string subject, string body)
		{
			var emailSettings = _configuration.GetSection("EmailSettings");
			var smtpClient = new SmtpClient(emailSettings["SmtpServer"])
			{
				Port = int.Parse(emailSettings["Port"]),
				Credentials = new NetworkCredential(emailSettings["SenderEmail"], emailSettings["Password"]),
				EnableSsl = true
			};

			var mailMessage = new MailMessage
			{
				From = new MailAddress(emailSettings["SenderEmail"], emailSettings["SenderName"]),
				Subject = subject,
				Body = body,
				
			};
			mailMessage.To.Add(to);

			await smtpClient.SendMailAsync(mailMessage);
		}
	}
	
}
