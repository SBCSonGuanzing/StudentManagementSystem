using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StudentSystem.Shared.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Role { get; set; }
        public string? Avatar { get; set; } = string.Empty;

        [JsonIgnore]
        public List<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>();
        // Reference Student Model
        public Student? Student { get; set; }
        public Professor? Professor { get; set; }
    }
}
