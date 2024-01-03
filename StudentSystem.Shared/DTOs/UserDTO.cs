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

        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]

        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]

        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Role is required.")]

        public string Role { get; set; }

        [Required(ErrorMessage = "Avatar is required.")]

        public string? Avatar { get; set; } = string.Empty;

        // Reference StudentDTO

        public UserDetailsDTO? UserDetails { get; set; } = new UserDetailsDTO();

    }
}
