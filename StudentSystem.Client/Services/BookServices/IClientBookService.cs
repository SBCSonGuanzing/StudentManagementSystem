using StudentSystem.Shared.DTOs;
using StudentSystem.Shared.Models;

namespace StudentSystem.Client.Services.BookServices
{
    public interface IClientBookService
    {
        List<Book> books { get; set; }
        Task<List<Book>> GetAllBooks();
        Task<Book> GetSingleBook(int id);
        Task<int> AddBook(BookDTO book);
        Task UpdateBook(int id, BookDTO book);
        Task<List<Book>> DeleteBook(int id);

    }
}
