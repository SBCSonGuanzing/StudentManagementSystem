
using Azure.Core;
using StudentSystem.Shared.DTOs;
using StudentSystem.Shared.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using StudentSystem.Server.Data;

namespace StudentSystem.Server.Services.AuthServices
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private IHttpContextAccessor _httpContextAccessor;

        public AuthService(DataContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<User>> GetAllUsers()
        {
            var users = await _context.Users
               .Include(p => p.Student)
               .Include(p => p.Professor)
               .ToListAsync();
            return users;
        }

        public async Task<string> Login(LoginDTO request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(e => e.Email == request.Email.Trim());

            if (user == null)
            {
                return "No User Found";
            }

            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return "Wrong password.";
            }

            var token = CreateToken(user);

            return token;
        }


        public async Task<User> Register(UserDTO request)
        {
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            User new_user = new User
            {
                Email = request.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = request.Role
            };

            if(request.Role == "Admin")
            {
                new_user.Avatar = request.Avatar;
            }

            else if (request.Role == "Student")
            {
                Student student_details = new Student
                {
                    FirstName = request.UserDetails.FirstName,
                    LastName = request.UserDetails.LastName,
                    Contact = request.UserDetails.Contact,
                    Address = request.UserDetails.Address,
                    BirthDate = request.UserDetails.BirthDate,
                    Image = request.UserDetails.Image,
                    User = new_user,
                    UserId = new_user.Id
                };
                _context.Students.Add(student_details);
            }
            else if (request.Role == "Professor")
            {
                Professor professor_details = new Professor
                {
                    FirstName = request.UserDetails.FirstName,
                    LastName = request.UserDetails.LastName,
                    Contact = request.UserDetails.Contact,
                    Address = request.UserDetails.Address,
                    BirthDate = request.UserDetails.BirthDate,
                    Image = request.UserDetails.Image,
                    User = new_user,
                    UserId = new_user.Id
                };
                _context.Professors.Add(professor_details);
            }

            _context.Users.Add(new_user);

            await _context.SaveChangesAsync();

            return new_user;
        }

        private void CreatePasswordHash(string password, out byte[] PasswordHash, out byte[] PasswordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                PasswordSalt = hmac.Key;
                PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] PasswordHash, byte[] PasswordSalt)
        {
            using (var hmac = new HMACSHA512(PasswordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(PasswordHash);
            }
        }
        public string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            // 403 Don't have the Correct Role 
            // 401 No Autherization Header Set

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("Token").Value!));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken
                (
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: cred
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        public async Task<User> GetSingleUser(int id)
        {
            var users = await _context.Users
                     .Where(p => p.Id == id)
                     .FirstOrDefaultAsync();
            if (users == null)
                return null;

            return users;
        }
    }
}
