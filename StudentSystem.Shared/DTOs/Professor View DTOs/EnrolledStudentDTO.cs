using StudentSystem.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Shared.DTOs
{
    public class EnrolledStudentDTO
    {
        public Subject Subject { get; set; }
        public int SubjectId { get; set; }
        public EnrollmentStudentDTO Enrollment { get; set; }
        public int EnrollmentId { get; set; }
        public ProfessorDTO Professor { get; set; }
        public int? ProfessorId { get; set; }
    }
}
