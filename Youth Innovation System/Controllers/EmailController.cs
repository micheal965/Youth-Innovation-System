using Microsoft.AspNetCore.Mvc;
using Youth_Innovation_System.Core.IServices;
using Youth_Innovation_System.Shared.ApiResponses;

namespace Youth_Innovation_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }
        [HttpPost("Send-Email")]
        public async Task<IActionResult> SendEmail(string to, string subject, string body)
        {
            await _emailService.SendEmailAsync(to, subject, body);
            return Ok(new ApiResponse(StatusCodes.Status200OK, "Email Sent Successfully!"));
        }



    }
}
