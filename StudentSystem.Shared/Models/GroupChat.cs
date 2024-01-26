using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Shared.Models
{
    public class GroupChat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<User> Members { get; set; } = new List<User>();
        public int OwnerId { get; set; } 
        public List<GroupChatMessage> Messages { get; set; } = new List<GroupChatMessage>();
        
    }
}
