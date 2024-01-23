using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Shared.DTOs
{
    public class AddUserToGroupDTO
    {
        public int UserId {  get; set; }
        public int GroupChatId { get; set; }
    }
}
 