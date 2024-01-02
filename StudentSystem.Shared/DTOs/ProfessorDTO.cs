using StudentSystem.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Shared.DTOs
{
    public class ProfessorDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
        public DateTime? BirthDate { get; set; } = DateTime.Now;
        public string Image { get; set; } = string.Empty;
        public List<EnrolledStudentDTO>? EnrolledSubjects { get; set; }

    }
}
