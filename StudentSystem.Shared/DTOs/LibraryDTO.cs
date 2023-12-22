using StudentSystem.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Shared.DTOs
{
    public class LibraryDTO
    {
        public List<BorrowedBooksDTO> BorrowedBooks { get; set; }
        public DateTime? DateBorrowed { get; set; } = DateTime.Now;
        public DateTime? DateReturn { get; set; } = DateTime.Now;
        public string BorrowedReason { get; set; }
        public int StudentId { get; set; }

    }
}
