using StudentSystem.Server.Data;
using StudentSystem.Shared.DTOs;

namespace StudentSystem.Server.Services.EnrolledSubjectsServices
{
    public class EnrolledSubjectsService : IEnrolledSubjectsService
    {
        // Dependency injection
        private readonly DataContext _context;

        // constructor to inject the DataContext again
        public EnrolledSubjectsService(DataContext context)
        {
            _context = context;
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
                        SubjectId = enrolledSub.SubjectId
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
                   .Include(p => p.Subject)
                   .Include(p => p.Enrollment)
                   .ToListAsync();
                return enrolled;
            }

        public async Task<List<EnrolledSubjects>> GetSingleEnrolledSubjects(int id)
        {
            var subject = await _context.EnrolledSubjects
                .Include(p=> p.Enrollment)
                    .ThenInclude(p=> p.Student)
                .Include(p => p.Subject)   
                .Include(p => p.Enrollment)
                .Where(p => p.Enrollment.StudentId== id)
                .ToListAsync();
            if (subject == null)
                return null;

            return subject;
        }
    }
}
