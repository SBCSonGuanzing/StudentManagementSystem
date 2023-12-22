using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Shared.Models
{
    public class BorrowedBooks
    {
        public int Id { get; set; }
        public Book Book { get; set; }
        public int BookId { get; set; }
        public Library Library { get; set; }
        public int LibraryId { get; set; }
    }
}
