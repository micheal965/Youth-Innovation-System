using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Youth_Innovation_System.Core.IServices.ChatServices;
using Youth_Innovation_System.Shared.ApiResponses;

namespace Youth_Innovation_System.Controllers.Chat
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }
        [Authorize]
        [HttpGet("Get-Chat-History/{receiverId}")]
        public async Task<IActionResult> GetChatHistory(string receiverId)
        {
            var senderId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                var messages = await _chatService.GetChatHistoryAsync(senderId, receiverId);
                return Ok(messages);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new ApiResponse(StatusCodes.Status401Unauthorized, ex.Message));
            }
        }
        [Authorize]
        [HttpDelete("Delete-Message/{messageId}")]
        public async Task<IActionResult> DeleteMessage(int messageId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _chatService.DeleteMessageAsync(messageId, userId);

            if (!result)
                return NotFound(new ApiResponse(StatusCodes.Status404NotFound, "Message not found or you don't have permission to delete it."));

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Message deleted successfully."));
        }
    }
}
