using Microsoft.AspNetCore.Components;
using MudBlazor;
using StudentSystem.Shared.DTOs;
using StudentSystem.Shared.Models;
using System.Net.Http.Json;


namespace StudentSystem.Client.Services.BorrowedBooksServices
{
    public class ClientBorrowedBooksService : IClientBorrowedBooksService
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;
        private readonly ISnackbar _snackbar;

        public ClientBorrowedBooksService(HttpClient httpClient, NavigationManager navigationManager, ISnackbar snackbar)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
            _snackbar = snackbar;
        }
        public List<BorrowedBooks> borrowedBooks { get; set; } = new List<BorrowedBooks>();

        public async Task<int> AddBorrowedBook(LibraryDTO request)
        {

            HttpResponseMessage? status = await _httpClient.PostAsJsonAsync("api/BorrowedBooks/add-borrowed-book", request);
            if(status.IsSuccessStatusCode)
            {
                _snackbar.Add(
                   "Successfully Borrowed Book",
                   Severity.Success,
                   config =>
                   {
                       config.ShowTransitionDuration = 200;
                       config.HideTransitionDuration = 400;
                       config.VisibleStateDuration = 2500;
                   });

                _navigationManager.NavigateTo("/all-enrollment");
            } else
            {
                _snackbar.Add(
                   "Already Borrowed Book",
                   Severity.Warning,
                   config =>
                   {
                       config.ShowTransitionDuration = 200;
                       config.HideTransitionDuration = 400;
                       config.VisibleStateDuration = 2500;
                   });
                return 0;
            }

            return 0;
        }

        public async Task<List<BorrowedBooks>> GetAllBorrowedBooks()
        {
            var result = await _httpClient.GetFromJsonAsync<List<BorrowedBooks>>("api/BorrowedBooks/");
            if (result != null)
            {
                borrowedBooks = result;
            }
            return borrowedBooks;
        }

        public async Task<List<BorrowedBooks>> GetSingleBorrowedBook(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<List<BorrowedBooks>>($"api/BorrowedBooks/single-borrow/{id}");
            if (result == null)
            {
                borrowedBooks = result;
            }
            return result;
        }

    }
}
