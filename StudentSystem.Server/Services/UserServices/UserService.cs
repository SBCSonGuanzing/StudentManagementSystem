using Blazorise.Extensions;
using MudBlazor.Extensions;
using StudentSystem.Server.Data;
using System.Security.Claims;

namespace StudentSystem.Server.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<User>?> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user is null)
                return null;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return await _context.Users.ToListAsync();
        }

        public async Task<string> GetSingleUser()
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var users = await _context.Users
                     .Where(p => p.Id.ToString() == userId)
                      .Select(p => p.Id.ToString())
                     .FirstOrDefaultAsync();

            return users;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var users = await _context.Users
               .Include(p => p.Student)
               .Include(p => p.Professor)
               .ToListAsync();
            return users;
        }

        public async Task<Student?> GetSingleStudent()
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if(userId.IsNullOrEmpty()) return null;

            var student = await _context.Users
                      .Where(p => p.Id.ToString() == userId)
                      .Select(p => p.Student)
                      .FirstOrDefaultAsync();

            return student;
        }

        
        public async Task<string> GetUserRole()
        {
            var userRole = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Role)?.Value;
            var users = await _context.Users
                    .Where(p => p.Role == userRole)
                     .Select(p => p.Role)
                    .FirstOrDefaultAsync();

            return users;
        }

        public async Task<string> GetUserId()
        {
            var userRole = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var users = await _context.Users
                    .Where(p => p.Id.ToString() == userRole)
                     .Select(p => p.Id.ToString())
                    .FirstOrDefaultAsync();

            return users;
        }

        public async Task<string> GetUserEmail()
        {
            var userEmail = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Name)?.Value;
            var users = await _context.Users
                    .Where(p => p.Id.ToString() == userEmail)
                     .Select(p => p.Email)
                    .FirstOrDefaultAsync();

            return users;
        }
    }
}
