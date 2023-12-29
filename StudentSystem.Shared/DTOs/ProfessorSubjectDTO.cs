using StudentSystem.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Shared.DTOs
{
    public class ProfessorSubjectDTO
    {
        public Professor Professor { get; set; }
        public int ProfessorId { get; set; }
    }
}
