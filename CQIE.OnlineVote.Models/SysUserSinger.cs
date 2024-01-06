using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQIE.OnlineVote.Models
{
    public class SysUserSinger
    {
        public int Id { get; set; }
        public string? SingerName { get; set; }
        public string? SingerPhoto { get; set; }
        public string? SingerAge { get; set; }
        public bool? Sex {  get; set; }
        public string? SingerDescribe {  get; set; }
        public string? Motto { get; set; }
        public int CompetitionsId {  get; set; }
        public bool? Status { get; set;}

        public  Competition? Competitions { get; set; }

  
    }
}
