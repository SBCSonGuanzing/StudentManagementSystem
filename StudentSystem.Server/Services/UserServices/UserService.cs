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

        public async Task<int> GetSingleStudent(int id)
        {
            var student = await _context.Students
                      .Where(p => p.UserId == id)
                      .Select(p => p.Id)
                      .FirstOrDefaultAsync();
           
            return student;
        }
    }
}
