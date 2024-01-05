using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentSystem.Server.Services.BookServices;
using StudentSystem.Shared.DTOs;

namespace StudentSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost]
        public async Task<ActionResult<int>> AddBook(BookDTO book)
        {
            var result =  await _bookService.AddBook(book);
            if (result == 0)
            {
                return BadRequest(result);
            } else
            {
                return Ok(result);
            }   
        }

        //[HttpPost]
        //public async Task<ActionResult<int>> AddSubject(SubjectDTO subject)
        //{
        //    var result = await _subjectService.AddSubject(subject);

        //    if (result == 0) return BadRequest(result);
        //    return Ok(result);
        //}


        [HttpGet]

        public async Task<ActionResult<List<Book>>> GetAllBooks()
        {
            var result = await _bookService.GetAllBooks();
            return Ok(result);
        }

        [HttpDelete("delete-book/{id}")]
        public async Task<ActionResult<Book>> DeleteBook(int id)
        {
            var result = await _bookService.DeleteBook(id);
            if (result is null)
            {
                return NotFound("Book not found");
            }

            return Ok(result);
        }

        // Update a Hero
        [HttpPut("update-book/{id}")]
        public async Task<ActionResult<List<Book>>> UpdateBook(int id, BookDTO request)
        {
            var result = await _bookService.UpdateBook(id, request);
            if (result is null)
                return NotFound("Book not found.");
            return Ok(result);
        }

        // Get a Single SuperHero 
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetSingleBook(int id)
        {
            var result = await _bookService.GetSingleBook(id);
            if (result is null)
                return NotFound(" Book not found.");
            return Ok(result);
        }
    }
}