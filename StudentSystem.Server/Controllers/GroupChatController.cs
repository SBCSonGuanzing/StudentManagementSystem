using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using StudentSystem.Server.Services.GroupChatServices;
using StudentSystem.Shared.DTOs;
using StudentSystem.Shared.Models;

namespace StudentSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupChatController : ControllerBase
    {
        private readonly IGroupChatService _chatService;

        public GroupChatController(IGroupChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpGet("get-convo/{groupChatId}")]
        public async Task<ActionResult<List<GroupChatMessage>>> GetConversationAsync(int groupChatId)
        {
            List<GroupChatMessage> result = await _chatService.GetConversationAsync(groupChatId);
            if (result is null)
            {
                return BadRequest("No Conversation Found");
            }
            return Ok(result);
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

        [HttpPost]
        public async Task SaveMessagesAsyc(GroupChatMessage message)
        {
            await _chatService.SaveMessagesAsyc(message);
        }

        [HttpPost("create-group")]
        public async Task<ActionResult<List<GroupChat>>> CreateGroupChat(GroupChatDTO request)
        {
            List<GroupChat> result = await _chatService.CreateGroupChat(request);
            if (result is null)
                return NotFound("User not found.");
            return Ok(result);
        }

        [HttpPost("add-user-to-group")]
        public async Task<ActionResult<bool>> AddUserToGroup(int userId, int groupChatId)
        {
            var result = await _chatService.AddUserToGroup(userId, groupChatId);

            if (!result)
            {
                return BadRequest("Failed to add user to the group.");
            }
            return Ok(true);
        }

        [HttpDelete("remove-user-to-group")]
        public async Task<ActionResult<bool>> RemoveUserToGroup(int userId, int groupChatId)
        {
            var result = await _chatService.RemoveUserToGroup(userId, groupChatId);

            if (!result)
            {
                return BadRequest("Failed to remove user to the group.");
            }
            return Ok(true);
        }

        [HttpGet("users-from-group/{groupChatId}")]
        public async Task<ActionResult<List<User>>> GetGroupChatMembers(int groupChatId)
        {
            var result = await _chatService.GetGroupChatMembers(groupChatId);

            if (result.Count == 0)
            {
                return BadRequest("No Group Chat Members Found");
            }

            return Ok(result);
        }

        [HttpGet("all-groups")]
        public async Task<ActionResult<List<GroupChat>>> GetAllGroup()
        {
            var result = await _chatService.GetAllGroup();
            if(result.Count == 0)
            {
                return BadRequest("No Group Chat Found");
            }
            return Ok(result);  
        }

        [HttpDelete("delete-group/{id}")]
        public async Task<ActionResult<List<GroupChat>>> RemoveGroupChat(int groupChatId)
        {
            var result = await _chatService.RemoveGroupChat(groupChatId);
            if(result != null)
            {
                return Ok(result);
            }

            return NotFound("No Group Chat Deleted");
        }
    }
}