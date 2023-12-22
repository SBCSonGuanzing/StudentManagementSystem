using Microsoft.EntityFrameworkCore;
using StudentSystem.Server.Data;
using StudentSystem.Shared.DTOs;
using StudentSystem.Shared.Models;
using System.Net;

namespace StudentSystem.Server.Services.BorrowedBooksServices
{
    public class BorrowedBooksService : IBorrowedBooksService
    {
        private readonly DataContext _context;

        public BorrowedBooksService(DataContext dataContext)
        {
            _context = dataContext;
        }
        public async Task<int> AddBorrowedBooks(LibraryDTO request)
        {

            // Make an instance of Library Object and Populate it's field

            var borrowedBook = new Library
            {
                // Instance of Borrowed Books List (Contains ID)
                BorrowedBooks = new List<BorrowedBooks>(),
                DateBorrowed = DateTime.Now,
                DateReturn = DateTime.Now,
                BorrowedReason = request.BorrowedReason,
                StudentId = request.StudentId
            };

            // iterate everytime a book is added 
            // populate the borrowedbooks list

            foreach (var borrow in request.BorrowedBooks)
            {
                //bool alreadyBorrowed = await IsBookAlreadyBorrowed(request.StudentId, borrow.BookId);

                int alreadyBorrowed =  await _context.BorrowedBooks
                    .Where(b => b.Library.StudentId == request.StudentId &&                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            b.BookId == borrow.BookId)
                    .Select(b => b.Id)
                    .FirstOrDefaultAsync();

                if (alreadyBorrowed == 0)
                {
                    
                    var borrowed = new BorrowedBooks
                    {
                        BookId = borrow.BookId
                    };

                    borrowedBook.BorrowedBooks.Add(borrowed);
                    _context.BorrowedBooks.Add(borrowed);
                } else
                {
                    return 0;
                }

            }
            _context.Libraries.Add(borrowedBook);
            await _context.SaveChangesAsync();
            return borrowedBook.Id;
        }

     
        public async Task<List<BorrowedBooks>> GetAllBorrowedBooks()
        {
            // Get all the Book and Library ID
            var borrowed = await _context.BorrowedBooks
                  .Include(p => p.Book)
                  .Include(p => p.Library)
                  .ToListAsync();
            return borrowed;
        }

        public async Task<List<BorrowedBooks>> GetSingleBorrowedBook(int id)
        {
            var borrow = await _context.BorrowedBooks
                .Include(p => p.Library)
                    .ThenInclude(p => p.Student)
                .Include(p => p.Book)
                .Where(p => p.Library.StudentId == id)
                .ToListAsync();

            if (borrow == null)
                return null;

            return borrow;
        }

      
    }
}
