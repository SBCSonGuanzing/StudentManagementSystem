using StudentSystem.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Shared.DTOs
{
    public class SubjectDTO
    {
        public string Name { get; set; }
        public List<int> ProfessorIds { get; set; }

    }
}
