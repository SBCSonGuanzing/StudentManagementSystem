using StudentSystem.Shared.Models;

namespace StudentSystem.Client.Services.UserServices
{
    public interface IClientUserService
    {
        List<User> users { get; set; }
        List<EnrolledSubjects> professorStudents { get; set; }

        Task<List<User>> DeleteUser(int id);
        Task<List<User>> GetAllUser();
        Task<string> GetSingleUser();
        Task<Student?> GetSingleStudent();
        Task<Professor?> GetSingleProfessor();

        Task<string> GetUserRole();
        Task<string> GetUserId();
        Task<string> GetUserEmail();

        //Task<string> GetToken();
    }
}
