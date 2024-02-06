using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentSystem.Server.Services.BigBrotherServices;
using StudentSystem.Shared.DTOs;

namespace StudentSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BigBrotherController : ControllerBase
    {
        private readonly IBigBrotherService _brotherService;

        public BigBrotherController(IBigBrotherService brotherService)
        {
            _brotherService = brotherService;
        }

        [HttpGet("all-groups/{id}")]
        public async Task<ActionResult<List<GroupChat>>> GetAllGroup(int id)
        {
            var result = await _brotherService.GetAllGroup(id);
            if (result == null)
            {
                return BadRequest("No Group Chat Found");
            }
            return Ok(result);
        }

        [HttpGet("get-convo/{groupChatId}/{id}")]
        public async Task<ActionResult<List<GroupChatMessage>>> GetConversationAsync(int groupChatId, int id)
        {
            List<GroupChatMessage> result = await _brotherService.GetConversationAsync(groupChatId, id);
            if (result is null)
            {
                return NotFound("No Conversation Found");
            }
            return Ok(result);
        }

        [HttpGet("users-from-group/{groupChatId}/{id}")]
        public async Task<ActionResult<GetChatMembersDTO>> GetGroupChatMembers(int groupChatId, int id)
        {
            var result = await _brotherService.GetGroupChatMembers(groupChatId, id);

            if (result.users.Count == 0)
            {
                return NotFound("No Group Chat Members Found");
            }

            return Ok(result);
        }
    }
}
