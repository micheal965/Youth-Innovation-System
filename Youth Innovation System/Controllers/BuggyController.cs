using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Youth_Innovation_System.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : ControllerBase
    {

        [HttpGet("InternalServerError")]
        public async Task<ActionResult> GetServerError() => throw new Exception();
        [HttpGet("NotFound")]
        public async Task<ActionResult> GetNotFoundRequest() => NotFound();

        [HttpGet("BadRequest")]
        public async Task<ActionResult> GetBadRequest() => BadRequest();
        //Validation
        [HttpGet("BadRequest/{Id}")]
        public async Task<ActionResult> GetBadRequest(int Id) => Ok();

    }
}
