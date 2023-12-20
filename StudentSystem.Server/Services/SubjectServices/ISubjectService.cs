using StudentSystem.Shared.DTOs;

namespace StudentSystem.Server.Services.SubjectServices
{
    public interface ISubjectService
    {
        Task<List<Subject>> GetAllSubjects();
        Task<Subject> GetSingleSubject(int id);
        Task<List<Subject>> AddSubject(SubjectDTO subject);
        Task<List<Subject>> DeleteSubject(int id);
        Task<List<Subject>> UpdateSubject(int id, SubjectDTO request);
    }
}
