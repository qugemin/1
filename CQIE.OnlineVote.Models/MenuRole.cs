using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQIE.OnlineVote.Models
{
    public class MenuRole
    {
        public int Id { get; set; }
        public int RoleId {  get; set; }
        public int MenuId { get; set; }
        public SystemMenu? Menus { get; set; }
         public SysRole? Roels {  get; set; } 
    }
}
