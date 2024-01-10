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
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
        public string SenderRole { get; set; }
        public int SenderId { get; set; }
        public int RecipientId { get; set; }
    }
}
