using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQIE.OnlineVote.Models
{
    public class Competition//赛事
    {
        public int Id { get; set; }
        public string? ThemeName {  get; set; }
        public string? Describe {  get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool? Status { get; set; }
        public ICollection<SysUserSinger>? SysUserSingers { get; set; }
        public ICollection<CompetitionRound>? CompetitionRounds { get; set; }
    }
}
