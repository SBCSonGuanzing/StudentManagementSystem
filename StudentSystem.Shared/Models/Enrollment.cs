using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StudentSystem.Shared.Models
{
    public class Enrollment
    {
        public int Id { get; set; }

        [JsonIgnore]
        public List<EnrolledSubjects> EnrolledSubjects { get; set; }
        public DateTime? DateCreated { get; set; } = DateTime.Now;
        public string Semester { get; set; }
        public int SchoolYear { get; set; }
        public Student Student { get; set; }
        public int StudentId { get; set; }
    }
}
