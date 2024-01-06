using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQIE.OnlineVote.Models
{
    public class CompetitionRound//比赛轮次
    {
       public int Id { get; set; }
       public string? RoundName {  get; set; }
        public int CompetitionsId { get; set; }
       public bool? Status { get; set; }
       public Competition? Competitions { get; set; }
       public ICollection<Vote>? Votes { get; set; }

    }
}
