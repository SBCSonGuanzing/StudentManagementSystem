using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StudentSystem.Shared.Models
{
    public class ChatMessage
    {
        public int Id { get; set; } 
        public string Content {  get; set; }
        public DateTime Timestamp { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        public int UserId { get; set; }

    }
}
