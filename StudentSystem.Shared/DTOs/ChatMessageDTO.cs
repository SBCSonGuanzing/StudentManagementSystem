using StudentSystem.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Shared.DTOs
{
    public class ChatMessageDTO
    {
        public int Id { get; set; }
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        public DateTime Timestamp { get; set; }
        public string Content { get; set; }
        public UserDTO User { get; set; }
        public int UserId { get; set; }
    }
}
