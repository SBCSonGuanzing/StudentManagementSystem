using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentSystem.Server.Services.BorrowedBooksServices;
using StudentSystem.Shared.DTOs;

namespace StudentSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class BorrowedBooksController : ControllerBase
    {
        private readonly IBorrowedBooksService _borrowedBooksService;

        public BorrowedBooksController(IBorrowedBooksService borrowedBooksService)
        {
            _borrowedBooksService = borrowedBooksService;
        }

        [HttpPost("add-borrowed-book")]
        public async Task<ActionResult<int>> AddBorrowedBook(LibraryDTO request)
        {
            var result = await _borrowedBooksService.AddBorrowedBooks(request);
            if(result == 0)
            {
                return BadRequest(result);
            }
            return Ok(result); 
        }

        [HttpGet]
        public async Task<ActionResult<List<BorrowedBooks>>> GetAllBorrowedBook()
        {
            return await _borrowedBooksService.GetAllBorrowedBooks();
        }


        [HttpGet("single-borrow/{id}")]
        public async Task<ActionResult<List<BorrowedBooks>>> GetSingleBorrowedBook(int id)
        {
            var result = await _borrowedBooksService.GetSingleBorrowedBook(id);

            return result;
        }
    }
}
