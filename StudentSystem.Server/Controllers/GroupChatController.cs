using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using StudentSystem.Server.Hubs;
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
        private readonly IHubContext<ChatHub, IGroupChatClient> _hubContext;

        public GroupChatController(IGroupChatService chatService, IHubContext<ChatHub, IGroupChatClient> hubContext)
        {
            _chatService = chatService;
            _hubContext = hubContext;
        }

        [HttpGet("get-convo/{groupChatId}")]
        public async Task<ActionResult<List<GroupChatMessage>>> GetConversationAsync(int groupChatId)
        {
            List<GroupChatMessage> result = await _chatService.GetConversationAsync(groupChatId);
            if (result is null)
            {
                return NotFound("No Conversation Found");
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
        public async Task<ActionResult<List<GroupChatMessage>>> SaveMessagesAsyc(GroupChatMessage message)
        {
            var result = await _chatService.SaveMessagesAsync(message);

            if(result == null)
            {
                return BadRequest("No Messages Save");
            }
            return Ok(result);

        }

        [HttpPost("create-group")]
        public async Task<ActionResult<int>> CreateGroupChat(GroupChatDTO request)
        {
            var result = await _chatService.CreateGroupChat(request);

            if (result == 0)
            {
                return Unauthorized("Group chat already exists.");
            }
            else if (result == 0)
            {
                return NotFound("User not found.");
            }

            return Ok(result);
        }

        [HttpGet("get-group-name/{groupChatId}")]
        public async Task<ActionResult<string>> GetGroupName(int groupChatId)
        {
            var result = await _chatService.GetGroupName(groupChatId);
            if(result == null)
            {
                return NotFound("Group None Existing");
            }

            return Ok(result);
        }


        [HttpPost("add-user-to-group")]
        public async Task<ActionResult<GroupChat>> AddUserToGroup(AddUserToGroupDTO request)
        {
            var result = await _chatService.AddUserToGroup(request.UserId, request.GroupChatId);

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("Failed to add user to the group.");
            }
        }



        [HttpDelete("remove-user-to-group/{userId}/{groupChatId}")]
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
        public async Task<ActionResult<GetChatMembersDTO>> GetGroupChatMembers(int groupChatId)
        {
            var result = await _chatService.GetGroupChatMembers(groupChatId);

            if (result.users.Count == 0)
            {
                return NotFound("No Group Chat Members Found");
            }

            return Ok(result);
        }

        [HttpGet("all-groups")]
        public async Task<ActionResult<List<GroupChat>>> GetAllGroup()
        {
            var result = await _chatService.GetAllGroup();
            if(result == null)
            {
                return BadRequest("No Group Chat Found");
            }
            return Ok(result);  
        }

        [HttpGet("get-notgroup-members/{groupId}")]
        public async Task<ActionResult<List<User>>> GetNotMembers(int groupId)
        {
            var result = await _chatService.GetNotMembers(groupId);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound("No members found");
        }

        [HttpDelete("delete-group/{groupChatId}")]
        public async Task<ActionResult<List<GroupChat>>> RemoveGroupChat(int groupChatId)
        {
            var result = await _chatService.RemoveGroupChat(groupChatId);
            if(result != null)
            {
                return Ok(result);
            }

            return NotFound("No Group Chat Deleted");
        }

        [HttpPut("update-group/{id}")]

        public async Task<ActionResult<GroupChat?>> UpdateGroup(int id, GroupToUpdate groupName)
        {
            var result = await _chatService.UpdateGroupName(id, groupName);
            if(result != null)
            {
                return Ok(result);
            }

            return NotFound("No Group Chat Exist");
        }

        [HttpPut("update-status/{id}")]

        public async Task<ActionResult<bool>> UpdateStatus(int id, bool status)
        {
            var result = await _chatService.UpdateOnlineStatus(id, status);
            if (result == true)
            {
                return Ok(result);
            }

            return false;

        }

    }
}