using StudentSystem.Shared.Models;

namespace StudentSystem.Client.Services.UserServices
{
    public interface IClientUserService
    {
        List<User> users { get; set; }
        Task<List<User>> DeleteUser(int id);
        Task<List<User>> GetAllUser();
        Task<string> GetSingleUser();
        Task<Student?> GetSingleStudent();
        Task<int> GetSingleProfessor(int id);

        Task<string> GetUserRole();

    }
}
