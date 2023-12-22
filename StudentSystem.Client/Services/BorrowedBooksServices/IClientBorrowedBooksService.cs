using StudentSystem.Shared.DTOs;
using StudentSystem.Shared.Models;

namespace StudentSystem.Client.Services.BorrowedBooksServices
{
    public interface IClientBorrowedBooksService
    {
        List<BorrowedBooks> borrowedBooks { get; set; }
        Task<int> AddBorrowedBook(LibraryDTO request);
        Task<List<BorrowedBooks>> GetAllBorrowedBooks();
        Task<List<BorrowedBooks>> GetSingleBorrowedBook(int id);

    }
}
