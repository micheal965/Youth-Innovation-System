using Microsoft.AspNetCore.Mvc;
namespace Youth_Innovation_System.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : ControllerBase
    {

        [HttpGet("InternalServerError")]
        public async Task<IActionResult> GetServerError() => throw new Exception();
        [HttpGet("NotFound")]
        public async Task<IActionResult> GetNotFoundRequest() => NotFound();

        [HttpGet("BadRequest")]
        public async Task<IActionResult> GetBadRequest() => BadRequest();
        //Validation
        [HttpGet("BadRequest/{Id}")]
        public async Task<IActionResult> GetBadRequest(int Id) => Ok();

    }
}
