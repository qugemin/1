using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CQIE.OnlineVote.Models;

namespace CQIE.OnlineVote.Models
{
    public class BattleUser
    {
      public  int Id {  get; set; }
      public  int? UserId { get; set; }
      public  int? SingerId {  get; set; }
        public int? BattleId { get; set; }

        public Battle? Battle { get; set; }
      public Sysuser? User { get; set; }
    }
}
