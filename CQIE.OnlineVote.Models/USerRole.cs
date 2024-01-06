using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CQIE.OnlineVote.Models
{
    public class USerRole
    {
        public int Id { get; set; }
        public int? UId { get; set; }
        public int? RId { get; set; }

        public Sysuser? Sysusers { get; set; }

        public SysRole? Roles { get; set; }
  

    }
}
