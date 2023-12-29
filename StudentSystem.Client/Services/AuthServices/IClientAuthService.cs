using StudentSystem.Shared.DTOs;
using StudentSystem.Shared.Models;

namespace StudentSystem.Client.Services.AuthServices
{
    public interface IClientAuthService
    {
        Token token { get; set; }
        List<User> users { get; set; }
        Task<List<User>> GetAllUser();
        Task Register(UserDTO request);
        Task<string> Login(LoginDTO request);
        Task<string> GetSingleUser();

    }
}
