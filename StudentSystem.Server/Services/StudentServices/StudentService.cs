using StudentSystem.Server.Data;
using StudentSystem.Shared.DTOs;

namespace StudentSystem.Server.Services.StudentServices
{
    public class StudentService : IStudentService
    {
        private readonly DataContext _context;

        public StudentService(DataContext context)
        {
            _context = context;
        }
        public async Task<List<Student>?> DeleteStudent(int id)
        {
            var stud = await _context.Students.FindAsync(id);
            if (stud is null)
                return null;

            _context.Students.Remove(stud);
            await _context.SaveChangesAsync();


            return await _context.Students.ToListAsync();
        }
        public async Task<List<Student>> GetAllStudents()
        {
            var students = await _context.Students.ToListAsync();
            return students;
        }

        public async Task<Student> GetSingleStudent(int id)
        {
            var student = await _context.Students
                     .Where(p => p.Id == id)
                     .FirstOrDefaultAsync();
            if (student == null)
                return null;

            return student;
        }

        public async Task<List<Student>> UpdateStudent(int id, UserDetailsDTO request)
        {
            var student = await _context.Students
                .Where(student => student.Id == id)
                .FirstOrDefaultAsync();

            if (student == null)
                return null;

            student.FirstName = request.FirstName;
            student.LastName = request.LastName;
            student.BirthDate = request.BirthDate;
            student.Contact = request.Contact;
            student.Image = request.Image;
            student.Address = request.Address;

            await _context.SaveChangesAsync();

            return await GetAllStudents();
        }
    }
}
