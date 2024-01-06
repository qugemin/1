using CQIE.OnlineVote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace CQIE.OnlineVote.Services
{
    public class BattleServiceImp:IBattleService
    {
        private readonly CQIE.OnlineVote.Services.IBattleUserService _temp;
        private readonly CQIE.OnlineVote.DBManager.IDbManager _db;
        private readonly CQIE.OnlineVote.Services.IVoteService _vo;
        private readonly CQIE.OnlineVote.Services.ISysUserSingerService _singer;
        public BattleServiceImp(CQIE.OnlineVote.DBManager.IDbManager dbManager, CQIE.OnlineVote.Services.IBattleUserService temp, CQIE.OnlineVote.Services.IVoteService vo, CQIE.OnlineVote.Services.ISysUserSingerService singer)
        {
                    _db = dbManager;
                    _temp = temp;
                    _vo = vo;
                   _singer = singer;
        }          
       public  IQueryable<Battle> GetBattleList()
       {
            var query=from o in _db.LMS.Battle select o;
            return query;
       }
        public  string GetAdd(int SingerId2, int SingerId1)
        {

           var s = from o in _db.LMS.CompetitionRound.Where(o=>o.Status==true) select o;
           Battle battle1 = _db.LMS.Battle.Where(o=>o.Status==true).FirstOrDefault();
            if (battle1 != null)
            {
                return "上一对局还未结束";
            }
            SysUserSinger sysUserSinger = _db.LMS.SysUserSinger.Where(o => o.Id == SingerId1).FirstOrDefault();
            sysUserSinger.Status = false;
            SysUserSinger sysUserSinger1= _db.LMS.SysUserSinger.Where(o => o.Id == SingerId2).FirstOrDefault();
            sysUserSinger1.Status = false;
            Battle battle = new Battle();
            battle.SingerId1 = SingerId1;
            battle.SingerId2 = SingerId2;
           battle.Status = true;
            if (s.ToList().Count > 0)
            {
                _db.LMS.Battle.Add(battle);
                _db.LMS.UpdateRange(sysUserSinger, sysUserSinger1);
                _db.LMS.SaveChanges();
                return "添加成功";
            }
           return "比赛轮次尚未开始";
        }
        public bool Update(int Id)
        {
            var battless= _db.LMS.Battle.Where(o => o.Id == Id).Select(o => new {
                SingerId1=o.SingerId1,
                SingerId2=o.SingerId2,
            }).FirstOrDefault();
            var round = _db.LMS.CompetitionRound.Where(o => o.Status == true).Select(o => o.Id).FirstOrDefault();
            var battle =_db.LMS.Battle.Where(o=>o.Id==Id).FirstOrDefault();
            battle.Status = false;
            _db.LMS.Battle.Update(battle);
            Vote voteSinger1 = _vo.GetsingerId(Convert.ToInt32(battless.SingerId1), round);
            Vote voteSinger2 = _vo.GetsingerId(Convert.ToInt32(battless.SingerId2),round);
            if (voteSinger1 == null)
            {
                return false;
            }
            voteSinger1.EndScore = voteSinger1.Score + voteSinger1.Count * 10;
            voteSinger2.EndScore = voteSinger2.Score + voteSinger2.Count * 10;
            if (voteSinger1.EndScore > voteSinger2.EndScore)
            {

                     voteSinger2.Status = false;
                    _db.LMS.Vote.Update(voteSinger2);
                    _db.LMS.Vote.Update(voteSinger1);
                    _db.LMS.SaveChanges();
                     return true;
                
            }
            else if (voteSinger1.EndScore <voteSinger2.EndScore)
            {

                voteSinger1.Status = false;
                _db.LMS.Vote.Update(voteSinger1);
                    _db.LMS.Vote.Update(voteSinger2);
                    _db.LMS.SaveChanges();
                    return true;
               
            }
            else
            {
           
                if (voteSinger1.Score < voteSinger2.Score)
                {

                    voteSinger1.Status = false;
                    _db.LMS.Vote.Update(voteSinger1);
                        _db.LMS.Vote.Update(voteSinger2);
                        _db.LMS.SaveChanges();
                    return true;
                    
                }
                else
                {


                        voteSinger2.Status = false;
                      _db.LMS.Vote.Update(voteSinger2);
                        _db.LMS.Vote.Update(voteSinger1);
                        _db.LMS.SaveChanges();
                        return true;
                    

                }

            }
            return true;
        }
        public List<object>  BattleLists()
        {
            List<object> list = new List<object>();
            List<Battle> battles=_db.LMS.Battle.ToList();
            foreach (Battle battle in battles)
            {
                var  sysUserSinger=_db.LMS.SysUserSinger.Where(o=>o.Id==battle.SingerId1).Select(o=>o.SingerName).ToList().FirstOrDefault();
                var sysUserSingers= _db.LMS.SysUserSinger.Where(o => o.Id == battle.SingerId2).Select(o => o.SingerName).ToList().FirstOrDefault();
                var obectlist = new {
                    Id = battle.Id,
                    SingerId1 = sysUserSinger,
                    SIngerId2= sysUserSingers,
                    Status=battle.Status,

                };
                list.Add(obectlist);

            }
            return list;


        }
    }
}
