using Microsoft.AspNetCore.Components;
using MudBlazor;
using StudentSystem.Shared.DTOs;
using StudentSystem.Shared.Models;
using System.Net;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace StudentSystem.Client.Services.GroupChatServices
{
    public class ClientGroupChatService : IClientGroupChatService
    {
        private readonly HttpClient _httpClient;
        private readonly ISnackbar _snackbar;
        private readonly NavigationManager _navigationManager;

        public ClientGroupChatService(HttpClient httpClient, ISnackbar snackbar, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _snackbar = snackbar;
            _navigationManager = navigationManager;
        }
        public async Task<GroupChat> AddUserToGroup(AddUserToGroupDTO request)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/GroupChat/add-user-to-group/", request);

                if (response.IsSuccessStatusCode)
                {
                    var addedGroup = await response.Content.ReadFromJsonAsync<GroupChat>();

                    _snackbar.Add(
                        "Successfully Added User to Group",
                        Severity.Success,
                        config =>
                        {
                            config.ShowTransitionDuration = 200;
                            config.HideTransitionDuration = 400;
                            config.VisibleStateDuration = 2500;
                        });

                    return addedGroup;
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    _snackbar.Add(
                        "User already a Member",
                        Severity.Error,
                        config =>
                        {
                            config.ShowTransitionDuration = 200;
                            config.HideTransitionDuration = 400;
                            config.VisibleStateDuration = 2500;
                        });
                }
                else
                {
                    _snackbar.Add(
                        "Failed to add user to the group",
                        Severity.Error,
                        config =>
                        {
                            config.ShowTransitionDuration = 200;
                            config.HideTransitionDuration = 400;
                            config.VisibleStateDuration = 2500;
                        });
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                _snackbar.Add(
                    "An error occurred while adding user to the group",
                    Severity.Error,
                    config =>
                    {
                        config.ShowTransitionDuration = 200;
                        config.HideTransitionDuration = 400;
                        config.VisibleStateDuration = 2500;
                    });
            }

            return null; // Return null in case of an exception or failure
        }

        public async Task<List<GroupChat>> CreateGroupChat(GroupChatDTO request)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/GroupChat/create-group", request);

            if (response.IsSuccessStatusCode)
            {
                _snackbar.Add(
                    "Group chat created successfully",
                    Severity.Success,
                    config =>
                    {
                        config.ShowTransitionDuration = 200;
                        config.HideTransitionDuration = 400;
                        config.VisibleStateDuration = 2500;
                    });

                return await response.Content.ReadFromJsonAsync<List<GroupChat>>();
            }
            else if (response.StatusCode == HttpStatusCode.Conflict)
            {
                _snackbar.Add(
                    "Group chat already exists",
                    Severity.Warning,
                    config =>
                    {
                        config.ShowTransitionDuration = 200;
                        config.HideTransitionDuration = 400;
                        config.VisibleStateDuration = 2500;
                    });

                return null;
            }
            else
            {
                // Handle other error cases if needed
                _snackbar.Add(
                    "Error creating group chat",
                    Severity.Error,
                    config =>
                    {
                        config.ShowTransitionDuration = 200;
                        config.HideTransitionDuration = 400;
                        config.VisibleStateDuration = 2500;
                    });

                return null;
            }
        }


        public async Task<List<GroupChat>> GetAllGroup()
        {
            return await _httpClient.GetFromJsonAsync<List<GroupChat>>($"api/GroupChat/all-groups/");

        }

        public async Task<List<User>> GetNotMembers(int groupId)
        {
            var result = await _httpClient.GetFromJsonAsync<List<User>>($"api/GroupChat/get-notgroup-members/{groupId}");

            return result;
        }

        public async Task<List<GroupChatMessage>> GetConversationAsync(int groupChatId)
        {
            return await _httpClient.GetFromJsonAsync<List<GroupChatMessage>>($"api/GroupChat/get-convo/{groupChatId}");

        }

        public async Task<GetChatMembersDTO> GetGroupChatMembers(int groupChatId)
        {
            var result = await _httpClient.GetAsync($"api/GroupChat/users-from-group/{groupChatId}");

            if (result.StatusCode == HttpStatusCode.OK)
            {
                var users = await result.Content.ReadFromJsonAsync<GetChatMembersDTO>();
                return users;
            }

            return new GetChatMembersDTO();
        }

        public async Task<string> GetGroupName(int groupChatId)
        {
            var response = await _httpClient.GetAsync($"api/GroupChat/get-group-name/{groupChatId}");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return result;
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                // Handle 404 Not Found
                return null;
            }
            else
            {
                // Handle other error cases
                throw new Exception($"Error getting group name. Status code: {response.StatusCode}");
            }
        }


        public async Task<User> GetUserDetailsAsync(int userId)
        {
            var result = await _httpClient.GetAsync($"api/GroupChat/user-detail/{userId}");
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return await result.Content.ReadFromJsonAsync<User>();
            }
            return null;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            var data = await _httpClient.GetFromJsonAsync<List<User>>("api/GroupChat/users");
            return data;
        }

        public async Task<List<GroupChat>> RemoveGroupChat(int groupChatId)
        {
            var response = await _httpClient.DeleteAsync($"api/GroupChat/delete-group/{groupChatId}");

            if (response.IsSuccessStatusCode)
            {
                _snackbar.Add(
                    "User Deleted Succesfully",
                    Severity.Warning,
                    config =>
                    {
                        config.ShowTransitionDuration = 200;
                        config.HideTransitionDuration = 400;
                        config.VisibleStateDuration = 2500;
                    });

                return await response.Content.ReadFromJsonAsync<List<GroupChat>>();
            }
            else
            {
                // Handle other error cases if needed
                _snackbar.Add(
                    "User does not Exist",
                    Severity.Error,
                    config =>
                    {
                        config.ShowTransitionDuration = 200;
                        config.HideTransitionDuration = 400;
                        config.VisibleStateDuration = 2500;
                    });

                return null;
            }
        }

        public async Task<bool> RemoveUserToGroup(int userId, int groupChatId)
        {
            var response = await _httpClient.DeleteAsync($"api/GroupChat/remove-user-to-group/{userId}/{groupChatId}");

            if (response.IsSuccessStatusCode)
            {
                _snackbar.Add(
                    "User Deleted Succesfully",
                    Severity.Warning,
                    config =>
                    {
                        config.ShowTransitionDuration = 200;
                        config.HideTransitionDuration = 400;
                        config.VisibleStateDuration = 2500;
                    });

                return await response.Content.ReadFromJsonAsync<bool>();
            }
            else
            {
                // Handle other error cases if needed
                _snackbar.Add(
                    "User does not Exist",
                    Severity.Error,
                    config =>
                    {
                        config.ShowTransitionDuration = 200;
                        config.HideTransitionDuration = 400;
                        config.VisibleStateDuration = 2500;
                    });

                return false;
            }
        }

        public async Task SaveMessagesAsync(GroupChatMessage message)
        {
            var result = await _httpClient.PostAsJsonAsync("api/GroupChat", message);

            if(result.IsSuccessStatusCode)
            {
                _snackbar.Add(
                   "Save Message Succesfully!",
                   Severity.Success,
                   config =>
                   {
                       config.ShowTransitionDuration = 200;
                       config.HideTransitionDuration = 400;
                       config.VisibleStateDuration = 2500;
                   });
            } else
            {
                _snackbar.Add(
                   "Something, went wrong!, Refresh Page",
                   Severity.Error,
                   config =>
                   {
                       config.ShowTransitionDuration = 200;
                       config.HideTransitionDuration = 400;
                       config.VisibleStateDuration = 2500;
                   });
            }            
        }

        public async Task<GroupChat> UpdateGroup(int groupId, GroupToUpdate groupName)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/GroupChat/update-group/{groupId}", groupName);


            if (response.IsSuccessStatusCode)
            {
                _snackbar.Add(
                    "Group Renamed Succesfully",
                    Severity.Success,
                    config =>
                    {
                        config.ShowTransitionDuration = 200;
                        config.HideTransitionDuration = 400;
                        config.VisibleStateDuration = 2500;
                    });

                return await response.Content.ReadFromJsonAsync<GroupChat>();
            }
            else
            {
                // Handle other error cases if needed
                _snackbar.Add(
                    "Group does not Exist",
                    Severity.Error,
                    config =>
                    {
                        config.ShowTransitionDuration = 200;
                        config.HideTransitionDuration = 400;
                        config.VisibleStateDuration = 2500;
                    });

                return null;
            }
        }
    }
}
