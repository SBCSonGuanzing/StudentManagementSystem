using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Shared.DTOs
{
    public class UserDTO
    {

        public string Email { get; set; } = string.Empty;


        public string Password { get; set; } = string.Empty;

        public string Role { get; set; }

        public string? Avatar { get; set; } = string.Empty;

        // Reference StudentDTO

        public UserDetailsDTO? UserDetails { get; set; } = new UserDetailsDTO();

    }
}
