using StudentSystem.Shared.DTOs;

namespace StudentSystem.Server.Services.BookServices
{
    public interface IBookService
    {
        Task<List<Book>> GetAllBooks();
        Task<Book> GetSingleBook(int id);
        Task<int> AddBook(BookDTO book);
        Task<List<Book>> DeleteBook(int id);
        Task<List<Book>> UpdateBook(int id, BookDTO book);
    }
}
