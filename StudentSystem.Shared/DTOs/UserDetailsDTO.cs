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


        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        //[Required (ErrorMessage = "Phone number is invalid!")]
        //[RegularExpression("^(09|\\+639)\\d{9}$")]
        public string? Contact { get; set; }

        public string? Address { get; set; }

        public DateTime? BirthDate { get; set; } = DateTime.Now;
        public string? Image { get; set; } = string.Empty;

    }
}
