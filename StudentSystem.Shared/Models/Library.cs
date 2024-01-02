using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StudentSystem.Shared.Models
{
    public class Library
    {
        public int Id { get; set; }
        [JsonIgnore]
        public List<BorrowedBooks> BorrowedBooks { get; set; }
        public DateTime? DateBorrowed { get; set; } = DateTime.Now;
        public DateTime? DateReturn { get; set; } = DateTime.Now;
        public string BorrowedReason { get; set; }
        public Student Student { get; set; }
        public int StudentId { get; set; }

    }
}
