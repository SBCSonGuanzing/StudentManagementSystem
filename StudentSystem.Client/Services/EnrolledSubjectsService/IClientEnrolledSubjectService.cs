using StudentSystem.Shared.DTOs;
using StudentSystem.Shared.Models;

namespace StudentSystem.Client.Services.EnrolledSubjectsService
{
    public interface IClientEnrolledSubjectService
    {
        List<EnrolledSubjects> ClientEnrolledSubjects { get; set; }
        Task<int> AddEnrolledSubject(EnrollmentDTO request);
        Task<List<EnrolledSubjects>> GetAllEnrolledSubject();
        Task<List<EnrolledSubjects>> GetSingleEnrolledSubjects(int id);
        Task<List<EnrolledSubjects>> GetProfessorStudents();
        Task<List<EnrolledSubjects>> GetProfessorStudentsId(int id);
    }
}
