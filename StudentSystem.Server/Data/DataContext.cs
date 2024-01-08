global using StudentSystem.Shared.Models;
global using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace StudentSystem.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Test> test { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Professor> Professors { get; set;}
        public DbSet<Book> Books { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<EnrolledSubjects> EnrolledSubjects { get; set; }
        public DbSet<Library> Libraries { get; set; }
        public DbSet<BorrowedBooks> BorrowedBooks { get; set;}
        public DbSet<Announcement> Announcements { get; set; }
    }
}
