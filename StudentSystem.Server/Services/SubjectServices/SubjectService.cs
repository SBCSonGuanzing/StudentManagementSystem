using Azure.Core;
using Microsoft.EntityFrameworkCore;
using StudentSystem.Server.Data;
using StudentSystem.Server.Services.BorrowedBooksServices;
using StudentSystem.Shared.DTOs;
using StudentSystem.Shared.Models;


namespace StudentSystem.Server.Services.SubjectServices
{
    public class SubjectService : ISubjectService
    {
        private readonly DataContext _context;
        private readonly IBorrowedBooksService _borrowedBooksService;

        public SubjectService(DataContext context, IBorrowedBooksService borrowedBooksService)
        {
            _context = context;
            _borrowedBooksService = borrowedBooksService;
        }
        public async Task<int> AddSubject(SubjectDTO request)
        {
            Subject newSubject = new Subject()
            {
                Name = request.Name,
                Professors = new List<Professor>()
            };

            var subjectExisting = await _context.Subjects
                   .Where(subject => subject.Name == request.Name)
                   .Select(b => b.Name)
                   .FirstOrDefaultAsync();

            if(subjectExisting != request.Name)
            {
                foreach (int id in request.ProfessorIds)
                {
                    // TODO: Query Professor with selectectedProf.Id
                    Professor professor = _context.Professors.First(p => p.Id == id);
                    newSubject.Professors.Add(professor);
                }
            } 
            else
            {
                return 0;
            }
           
            _context.Add(newSubject);
            await _context.SaveChangesAsync();
            return newSubject.Id;
        }

        public async Task<List<Subject>> DeleteSubject(int id)
        {
            var subject = await _context.Subjects
                    .Where(p => p.Id == id)
                    .FirstOrDefaultAsync();
            if (subject is null)
                return null;

            _context.Remove(subject);
            await _context.SaveChangesAsync();
            return await GetAllSubjects();
        }

        public async Task<List<Subject>> GetAllSubjects()
        {
            // TODO: Include Professors
            var subjects = await _context.Subjects
                .Include(p => p.Professors)
                .ToListAsync();
            return subjects;
        }

        public async Task<Subject> GetSingleSubject(int id)
        {
            var subjects = await _context.Subjects
                     .Where(p => p.Id == id)
                     .Include(p => p.Professors)
                     .FirstOrDefaultAsync();
            if (subjects == null)
                return null;

            return subjects;
        }

        public async Task<List<Subject>> UpdateSubject(int id, SubjectDTO request)
        {
            var subjects = await _context.Subjects
                .Include(p => p.Professors)
                .Where(subject => subject.Id == id)
                .FirstOrDefaultAsync();

            if (subjects == null) 
                return null;

            subjects.Name = request.Name;
            subjects.Professors = new List<Professor>();

            foreach(int professorId in request.ProfessorIds)
            {
                Professor professor = _context.Professors.First(p => p.Id == professorId);
                subjects.Professors.Add(professor);
            }

            await _context.SaveChangesAsync();
            return await GetAllSubjects();

        }
    }
}
