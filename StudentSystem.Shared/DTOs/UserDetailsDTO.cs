using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Shared.DTOs
{
    public class UserDetailsDTO
    {
        [Required(ErrorMessage = "FirstName is required.")]

        public string? FirstName { get; set; }
        [Required(ErrorMessage = "LastName is required.")]

        public string? LastName { get; set; }
        [Required(ErrorMessage = "Contact is required.")]

        public string? Contact { get; set; }
        [Required(ErrorMessage = "Address is required.")]

        public string? Address { get; set; }

        public DateTime? BirthDate { get; set; } = DateTime.Now;
        public string? Image { get; set; } = string.Empty;

    }
}
