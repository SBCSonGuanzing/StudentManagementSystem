using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StudentSystem.Server.Data;
using StudentSystem.Server.Hubs;
using StudentSystem.Server.Services.AnnouncementServices;
using StudentSystem.Server.Services.AuthServices;
using StudentSystem.Server.Services.BookServices;
using StudentSystem.Server.Services.BorrowedBooksServices;
using StudentSystem.Server.Services.ChatServices;
using StudentSystem.Server.Services.EnrolledSubjectsServices;
using StudentSystem.Server.Services.GroupChatServices;
using StudentSystem.Server.Services.ProfessorServices;
using StudentSystem.Server.Services.StudentServices;
using StudentSystem.Server.Services.SubjectServices;
using StudentSystem.Server.Services.UserServices;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Register DB Context
string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSignalR();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer Scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                    .GetBytes(builder.Configuration.GetSection("Token").Value!)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        })
    ;
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IProfessorService, ProfessorService>();
builder.Services.AddScoped<ISubjectService, SubjectService>();
builder.Services.AddScoped<IEnrolledSubjectsService, EnrolledSubjectsService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IBorrowedBooksService, BorrowedBooksService>();
builder.Services.AddScoped<IAnnouncementService, AnnouncementService>();
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<IUserService,  UserService>();
builder.Services.AddScoped<IGroupChatService, GroupChatService>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddDbContext<DataContext>();   

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseWebAssemblyDebugging();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseAuthorization();
app.MapHub<AnnouncementHub>("announcementhub");
app.MapHub<ChatHub>("chat-hub");

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");
    
app.Run();
