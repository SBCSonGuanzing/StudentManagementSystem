using Blazored.LocalStorage;
using BlazorPlayGround.Client;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using StudentSystem.Client;
using StudentSystem.Client.Services.AuthServices;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddMudServices();

// Register the Authentication State Provider 
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

// Register IClientAuthService
builder.Services.AddScoped<IClientAuthService, ClientAuthService>();

builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
