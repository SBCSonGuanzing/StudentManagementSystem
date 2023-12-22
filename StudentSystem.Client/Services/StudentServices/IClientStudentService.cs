using StudentSystem.Shared.DTOs;
using StudentSystem.Shared.Models;

namespace StudentSystem.Client.Services.StudentServices
{
    public interface IClientStudentService
    {
        List<Student> students { get; set; }
        Task<List<Student>> GetAllStudents();
        Task<Student> GetSingleStudent(int id);
        Task DeleteStudent(int id);
        Task UpdateStudent(int id, Student student);
    }
}
    