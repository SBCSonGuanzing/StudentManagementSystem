using StudentSystem.Shared.DTOs;

namespace StudentSystem.Server.Services.AuthServices
{
    public interface IAuthService
    {
        Task<List<User>> GetAllUsers();
        Task<User> Register(UserDTO request);
        Task<string> Login(LoginDTO request);
        Task<string> GetSingleUser();

    }
}
