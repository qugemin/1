using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQIE.OnlineVote.Models
{
    public class SysuerInformation
    {
        public int Id { get; set; }
        public int? UID { get; set; }
        public string? Phone { get; set; }
        public Sysuser? Sysusers { get; set; }
    }
 
}
