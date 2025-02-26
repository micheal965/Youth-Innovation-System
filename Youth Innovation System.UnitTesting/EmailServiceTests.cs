//using Castle.Core.Smtp;
//using Microsoft.Extensions.Configuration;
//using Moq;
//using NUnit.Framework;
//using System.Net.Mail;
//using Youth_Innovation_System.Core.IServices;
//using Youth_Innovation_System.Service;

//namespace Youth_Innovation_System.UnitTesting
//{
//    [TestFixture]
//    public class EmailServiceTests
//    {
//        //private Mock<ISmtpClient> _smtpClientMock;
//        //private EmailService _emailService;
//        //private IConfiguration _configuration;
//        //[SetUp]
//        //public void Setup()
//        //{
//        //    var inMemorySettings = new Dictionary<string, string>{
//        //    { "EmailSettings:SmtpServer", "smtp.example.com" },
//        //    { "EmailSettings:Port", "587" },
//        //    { "EmailSettings:SenderEmail", "test@example.com" },
//        //    { "EmailSettings:SenderName", "Test Sender" },
//        //    { "EmailSettings:Password", "password123" }
//        //    };
//        //    _configuration = new ConfigurationBuilder()
//        //  .AddInMemoryCollection(inMemorySettings)
//        //  .Build();
//        //    _smtpClientMock = new Mock<IEmailService>();  // Create fake SMTP client
//        //    _emailService = new EmailService(_configuration, _smtpClientMock.Object); // Inject mocks
//    }
//}
//}