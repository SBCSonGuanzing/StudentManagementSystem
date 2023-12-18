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

        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Password is required.")]


        public string Password { get; set; } = string.Empty;
        [Required(ErrorMessage = "Role is required.")]

        public string Role { get; set; }

        // Reference StudentDTO

        public StudentDTO? StudentDTO { get; set; } = new StudentDTO();

    }
}
