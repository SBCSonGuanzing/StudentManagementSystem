﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StudentSystem.Shared.Models
{
    public class Professor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
        public DateTime? BirthDate { get; set; } = DateTime.Now;
        public string Image { get; set; } = string.Empty;

        [JsonIgnore]
        public User User { get; set; }
        public int UserId { get; set; }

        [JsonIgnore]
        public List<Subject> Subjects { get; set; }
        [JsonIgnore]
        public List<EnrolledSubjects>? EnrolledSubjects { get; set; }

      
    }
}
