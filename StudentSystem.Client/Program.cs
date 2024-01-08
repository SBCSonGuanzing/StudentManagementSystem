using Blazored.LocalStorage;
using BlazorPlayGround.Client;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.SignalR.Client;
using MudBlazor.Services;
using StudentSystem.Client;
using StudentSystem.Client.Services.AuthServices;
using StudentSystem.Client.Services.BookServices;
using StudentSystem.Client.Services.BorrowedBooksServices;
using StudentSystem.Client.Services.EnrolledSubjectsService;
using StudentSystem.Client.Services.ProfessorServices;
using StudentSystem.Client.Services.StudentServices;
using StudentSystem.Client.Services.SubjectServices;
using StudentSystem.Client.Services.UserServices;



var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddMudServices();

// Register the Authentication State Provider 
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

// Register IClientAuthService
builder.Services.AddScoped<IClientAuthService, ClientAuthService>();
builder.Services.AddScoped<IClientBookService, ClientBookService>();
builder.Services.AddScoped<IClientProfessorService, ClientProfessorService>();
builder.Services.AddScoped<IClientSubjectService, ClientSubjectService>();
builder.Services.AddScoped<IClientStudentService, ClientStudentService>();
builder.Services.AddScoped<IClientEnrolledSubjectService, ClientEnrolledSubjectService>();
builder.Services.AddScoped<IClientBorrowedBooksService, ClientBorrowedBooksService>();
builder.Services.AddScoped<IClientUserService, ClientUserService>();

builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
