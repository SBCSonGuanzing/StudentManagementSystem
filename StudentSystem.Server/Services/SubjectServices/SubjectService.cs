using Azure.Core;
using Microsoft.EntityFrameworkCore;
using StudentSystem.Server.Data;
using StudentSystem.Shared.DTOs;
using StudentSystem.Shared.Models;

namespace StudentSystem.Server.Services.SubjectServices
{
    public class SubjectService : ISubjectService
    {
        private readonly DataContext _context;
        public SubjectService(DataContext context)
        {
            _context = context;
        }
        public async Task<List<Subject>> AddSubject(SubjectDTO subject)
        {
            var newSubject = new Subject()
            {
                Name = subject.Name,
                Category = subject.Category,
                Description = subject.Description,
            };

            _context.Add(newSubject);
            await _context.SaveChangesAsync();
            return await GetAllSubjects();
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
            var subjects = await _context.Subjects.ToListAsync();
            return subjects;
        }

        public async Task<Subject> GetSingleSubject(int id)
        {
            var subjects = await _context.Subjects
                     .Where(p => p.Id == id)
                     .FirstOrDefaultAsync();
            if (subjects == null)
                return null;

            return subjects;
        }

        public async Task<List<Subject>> UpdateSubject(int id, SubjectDTO request)
        {
            var subjects = await _context.Subjects
                .Where(team => team.Id == id)
                .FirstOrDefaultAsync();

            if (subjects == null)
                return null;

            subjects.Name = request.Name;
            subjects.Description = request.Description;
            subjects.Category = request.Category;

            await _context.SaveChangesAsync();

            return await GetAllSubjects();
        }
    }
}
