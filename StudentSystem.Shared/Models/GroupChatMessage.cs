using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Shared.Models
{
    public class GroupChatMessage
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int GroupChatId { get; set; }
        public GroupChat GroupChat { get; set; }

    }
}
