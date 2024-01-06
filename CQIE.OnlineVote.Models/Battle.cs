using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQIE.OnlineVote.Models
{
    public class Battle
    { 

        public int Id { get; set; }
        public int? SingerId1 { get; set; }
        public int? SingerId2 { get; set; }

        public bool? Status { get; set; }
        public ICollection<BattleUser>? Temp { get; set; }

    }
}
