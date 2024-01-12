using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Shared.Models
{ 
    public class ChatMessage
    {
        public int Id { get; set; }
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        public DateTime Timestamp { get; set; }
        public string Content { get; set; }
        public User? User { get; set; }
        public int UserId { get; set; }
    }
}
