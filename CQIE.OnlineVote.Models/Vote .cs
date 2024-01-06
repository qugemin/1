using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQIE.OnlineVote.Models
{
    public class Vote
    {
        public int Id { get; set; }
        public int? RoundId {  get; set; }

        public int? RacedId { get; set; }
        public int? SingerId { get; set; }

        public int? JudgeId { get; set; }
        public double? Score { get; set; }
        public double? Count { get; set; }
        public double? EndScore { get; set; }

        public bool? Status { get; set; }

        public CompetitionRound? CompetitionRounds { get; set; }


    }
}
