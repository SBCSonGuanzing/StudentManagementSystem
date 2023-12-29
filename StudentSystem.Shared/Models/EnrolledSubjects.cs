using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Shared.Models
{
    public class EnrolledSubjects
    {
        public int Id { get; set; }
        public Subject Subject { get; set; }
        public int SubjectId { get; set; }
        public Enrollment Enrollment { get; set; }
        public int EnrollmentId { get; set; }
        public Professor Professor { get; set; }
        public int? ProfessorId { get; set;}
    }
}
