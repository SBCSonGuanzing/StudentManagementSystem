using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentSystem.Server.Services.ChatServices;
using System.Security.Claims;

namespace StudentSystem.Server.Controllers
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

        [HttpGet("user-detail/{id}")]
        public async Task<ActionResult<User>> GetUserDetailsAsync(int id)
        {
            var result = await _chatService.GetUserDetailsAsync(id);
            if (result is null)
                return NotFound("User not found.");
            return Ok(result);
        }

        [HttpGet("users")]
        public async Task<ActionResult<List<User>>> GetUsersAsync()
        {
            List<User> result = await _chatService.GetUsersAsync();
            if (result is null)
            {
                return BadRequest("No User Found");
            }
            return Ok(result);
        }

        [HttpGet("get-chat/{receiverId}")]
        public async Task<ActionResult<List<ChatMessage>>> GetConversationAsync(int receiverId)
        {
            List<ChatMessage> result = await _chatService.GetConversationAsync(receiverId);
            if(result is null)
            {
                return BadRequest("No Conversation Found");
            }
            return Ok(result);           
        }

        [HttpPost]
        public async Task SaveMessagesAsyc(ChatMessage message)
        {
            await _chatService.SaveMessagesAsyc(message);
        }


    }
}
