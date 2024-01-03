using Blazorise.Extensions;
using StudentSystem.Server.Data;
using StudentSystem.Shared.DTOs;
using StudentSystem.Shared.Models;
using System.Security.Claims;

namespace StudentSystem.Server.Services.EnrolledSubjectsServices
{
    public class EnrolledSubjectsService : IEnrolledSubjectsService
    {
        // Dependency injection
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        // constructor to inject the DataContext again
        public EnrolledSubjectsService(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<int> AddEnrolledSubjects(EnrollmentDTO request)
        {
            var enrollment = new Enrollment
            {
                DateCreated = DateTime.Now,
                EnrolledSubjects = new List<EnrolledSubjects>(),
                Semester = request.Semester,
                SchoolYear = request.SchoolYear,
                StudentId = request.StudentId
            };

            foreach (var enrolledSub in request.EnrolledSubjects)
            {

                int alreadyEnrolled = await _context.EnrolledSubjects
                   .Where(b => b.Enrollment.StudentId == request.StudentId && b.SubjectId == enrolledSub.SubjectId)
                   .Select(b => b.Id)
                   .FirstOrDefaultAsync();


                if (alreadyEnrolled == 0)
                {
                    var enrolled = new EnrolledSubjects
                    {
                        SubjectId = enrolledSub.SubjectId,
                        ProfessorId = enrolledSub.ProfessorId
                    };

                    enrollment.EnrolledSubjects.Add(enrolled);
                    _context.EnrolledSubjects.Add(enrolled);

                } else
                {
                    return 0;
                }
            }

            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync(); 
            return enrollment.Id;
        }

            public async Task<List<EnrolledSubjects>> GetAllEnrolledSubject()
            {
                var enrolled = await _context.EnrolledSubjects
                   .Include(e => e.Subject)
                        .ThenInclude(subject => subject.Professors)
                   .Include(e => e.Enrollment)
                   .ToListAsync();
                return enrolled;
            }

        public async Task<List<EnrolledSubjects>> GetProfessorStudentId(int id)
        {
            List<EnrolledSubjects> professorStudents = await _context.EnrolledSubjects
               .Where(p => p.Professor.UserId == id)
               .Include(p => p.Enrollment)
                   .ThenInclude(p => p.Student)
               .Include(p => p.Subject)
               .ToListAsync();

            return professorStudents;
        }

        public async Task<List<EnrolledSubjects>> GetProfessorStudents()
        {
            var professorId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (professorId.IsNullOrEmpty()) return null;

            List<EnrolledSubjects> professorStudents = await _context.EnrolledSubjects
                .Where(p => p.Professor.UserId.ToString() == professorId)
                .Include(p => p.Enrollment)
                    .ThenInclude(p => p.Student)
                .Include(p => p.Subject)
                .ToListAsync();

            return professorStudents; 
        }

        public async Task<List<EnrolledSubjects>> GetSingleEnrolledSubjects(int id)
        {
            var subject = await _context.EnrolledSubjects
                .Include(p=> p.Enrollment)
                    .ThenInclude(p=> p.Student)
                .Include(p => p.Subject)   
                    .ThenInclude(p => p.Professors)
                .Where(p => p.Enrollment.StudentId == id)
                .ToListAsync();
            if (subject == null)
                return null;

            return subject;
        }

    
    }
}
