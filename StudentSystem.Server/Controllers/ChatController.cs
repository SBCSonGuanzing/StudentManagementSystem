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

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            var result = await _chatService.GetAllUsers();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetSingleUser(int id)
        {
            var result = await _chatService.GetSingleUser(id);
            if (result is null)
                return NotFound("User not found.");
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<ChatMessage>>> GetConversationAsync(int id)
        {
            var result = await _chatService.GetConversationAsync(id);
            
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> SaveMessagesAsyc(ChatMessage message)
        {
            await _chatService.SaveMessagesAsyc(message);
            return Ok("Message sent Succesfully");
        }

    }
}
