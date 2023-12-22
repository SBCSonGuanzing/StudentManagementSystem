﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Shared.DTOs
{
    
    public class EnrollmentDTO
    {
        public List<EnrolledSubjectsDTO> EnrolledSubjects { get; set; }
        public DateTime? DateCreated { get; set; } = DateTime.Now;
        public string Semester { get; set; }
        public int SchoolYear { get; set; }
        public int StudentId { get; set; }
    }
}
