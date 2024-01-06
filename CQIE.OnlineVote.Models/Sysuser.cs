namespace CQIE.OnlineVote.Models
{
    public class Sysuser
    {
        public int Id { get; set; }
        public string? Account { get; set; }
        public string? Password {  get; set; }
        public bool? Status { get; set; }
        public ICollection<USerRole>? USerRoles { get; set; }
        public SysuerInformation? SyserInfos { get; set;}

        public ICollection<BattleUser>? Temp { get; set; }
    }
}
