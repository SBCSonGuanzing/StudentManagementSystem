using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Shared.DTOs
{
    public class GroupChatDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<int> MembersId { get; set; } = new List<int>();
    }
}
