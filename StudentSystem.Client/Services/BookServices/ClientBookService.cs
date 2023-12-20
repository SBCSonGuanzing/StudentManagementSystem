using Microsoft.AspNetCore.Components;
using StudentSystem.Shared.Models;
using System.Net.Http.Json;
using System.Net.Http;
using System.Net;
using StudentSystem.Shared.DTOs;

namespace StudentSystem.Client.Services.BookServices
{
    public class ClientBookService : IClientBookService
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;
        public ClientBookService(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
        }

        public List<Book> books { get; set; } = new List<Book>();

        public async Task AddBook(Book book)
        {
            await _httpClient.PostAsJsonAsync("api/Book", book);
            _navigationManager.NavigateTo("/all-books");
        }

        public async Task<List<Book>> DeleteBook(int id)
        {
            HttpResponseMessage? response = await _httpClient.DeleteAsync($"api/Book/delete-book/{id}");

            if (response != null && response.IsSuccessStatusCode)
            {
                List<Book>? content = await response.Content.ReadFromJsonAsync<List<Book>>();
                if (content != null)
                {
                    books = content;
                }

            }
            return books;
        }

        public async Task<List<Book>> GetAllBooks()
        {
            var result = await _httpClient.GetFromJsonAsync<List<Book>>($"api/Book");
            if (result != null)
            {
                books = result;
            }
            return result;
        }

        public async Task<Book> GetSingleBook(int id)
        {
            var result = await _httpClient.GetAsync($"api/Book/{id}");
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return await result.Content.ReadFromJsonAsync<Book>();
            }
            return null;
        }

        public async Task UpdateBook(int id, Book book)
        {
            await _httpClient.PutAsJsonAsync($"api/Book/update-book/{id}", book);
            _navigationManager.NavigateTo("/all-books");
        }
    }
}
