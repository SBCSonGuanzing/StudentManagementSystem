﻿using Azure.Core;
using Blazorise.Extensions;
using Microsoft.EntityFrameworkCore;
using StudentSystem.Server.Data;
using StudentSystem.Shared.DTOs;

namespace StudentSystem.Server.Services.BookServices
{
    public class BookService : IBookService
    {
        private readonly DataContext _context;
        public BookService(DataContext context)
        {
            _context = context;
        }
        public async Task<int> AddBook(BookDTO book)
        {
            Book newBook = new Book()
            {
                Name = book.Name,
            
            };

            var isAlreadyExistingBook = await _context.Books
                .Where(book => book.Name == newBook.Name)
                .Select(book => book.Name)
                .FirstOrDefaultAsync();

            if(isAlreadyExistingBook != book.Name)
            {
                _context.Add(newBook);
            }
            else
            {
                return 0;
            }

            await _context.SaveChangesAsync();
            return newBook.Id;
        }



        public async Task<List<Book>> DeleteBook(int id)
        {
            var book = await _context.Books
                    .Where(p => p.Id == id)
                    .FirstOrDefaultAsync();
            if (book is null)
                return null;

            _context.Remove(book);
            await _context.SaveChangesAsync();
            return await GetAllBooks();
        }

        public async Task<List<Book>> GetAllBooks()
        {
            var books = await _context.Books.ToListAsync();
            return books;
        }

        public async Task<Book> GetSingleBook(int id)
        {
            var books = await _context.Books
                     .Where(p => p.Id == id)
                     .FirstOrDefaultAsync();
            if (books == null)
                return null;

            return books;
        }

        public async Task<List<Book>> UpdateBook(int id, BookDTO request)
        {
            var books = await _context.Books
                .Where(team => team.Id == id)
                .FirstOrDefaultAsync();

            if (books == null)
                return null;

            books.Name= request.Name;
            

            await _context.SaveChangesAsync();

            return await GetAllBooks();
        }
    }
}
