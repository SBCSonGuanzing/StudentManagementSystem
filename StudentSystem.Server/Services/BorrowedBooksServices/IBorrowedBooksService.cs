using StudentSystem.Shared.DTOs;

namespace StudentSystem.Server.Services.BorrowedBooksServices
{
    public interface IBorrowedBooksService
    {
        // List of the Borrowed books
        Task<List<BorrowedBooks>> GetAllBorrowedBooks();

        // Add a borrowed book and return its BorrowedBooksID
        Task<int> AddBorrowedBooks(LibraryDTO request);

        // Get a Single Borrowed Book Record 
        Task<List<BorrowedBooks>> GetSingleBorrowedBook(int id);

    }
}
