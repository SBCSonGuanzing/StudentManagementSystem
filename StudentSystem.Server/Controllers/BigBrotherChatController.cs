using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentSystem.Server.Services.BigBrotherChatServices;

namespace StudentSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BigBrotherChatController : ControllerBase
    {
        private readonly IBigBrotherChatService _chatService;

        public BigBrotherChatController(IBigBrotherChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpGet("get-chat/{receiverId}/{sendersId}")]
        public async Task<ActionResult<List<ChatMessage>>> GetConversationAsync(int receiverId, int sendersId)
        {
            List<ChatMessage> result = await _chatService.GetConversationAsync(receiverId, sendersId);
            if (result is null)
            {
                return BadRequest("No Conversation Found");
            }
            return Ok(result);
        }
    }
}
