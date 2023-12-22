using StudentSystem.Shared.DTOs;

namespace StudentSystem.Server.Services.StudentServices
{
    public interface IStudentService
    {
        Task<List<Student>?> DeleteStudent(int id);
        Task<List<Student>> UpdateStudent(int id, UserDetailsDTO request);
        Task<Student> GetSingleStudent(int id);
        Task<List<Student>> GetAllStudents();

    }
}
