using CQIE.OnlineVote.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQIE.OnlineVote.Services
{
    public class VoteServiceImp:IVoteService
    {
        private readonly CQIE.OnlineVote.DBManager.IDbManager _db;
        private readonly CQIE.OnlineVote.Services.ICompetitonRoundService _Round;
        private readonly CQIE.OnlineVote.Services.ICompetitionService _Race;
        public VoteServiceImp(CQIE.OnlineVote.DBManager.IDbManager dbManager,CQIE.OnlineVote.Services.ICompetitonRoundService Round, CQIE.OnlineVote.Services.ICompetitionService Race)
        {
            _db = dbManager;
            _Round = Round;
            _Race = Race;
         
        }
        public Vote GetsingerId(int SingerId,int round)
        {
            Vote voue = _db.LMS.Vote.Where(o => o.SingerId == SingerId&&o.RoundId==round).FirstOrDefault();
            return voue;
        }
        public IQueryable<Vote> GetRoundId(int Id)
        {
            var vote = from o in _db.LMS.Vote.Where (o =>o.Status==true&&o.RoundId==Id) select o;
            foreach (var vo in vote.ToList()) {
                SysUserSinger sysUserSinger = _db.LMS.SysUserSinger.Where(o => o.Id == Convert.ToInt32(vo.SingerId)).FirstOrDefault();
                sysUserSinger.Status = true;
         
                _db.LMS.SysUserSinger.Update(sysUserSinger);
                _db.LMS.SaveChanges();

            }
            return vote;
        }
        public List<object> UerVote()
        {
            List<object> list = new List<object>();
            List<Battle> battles = _db.LMS.Battle.Where(o => o.Status == true).ToList();
            foreach (Battle battle in battles)
            {
                var sysUserSinger = _db.LMS.SysUserSinger.Where(o => o.Id == battle.SingerId1).Select(o => o.SingerPhoto).ToList().FirstOrDefault();
                var sysUserSingers = _db.LMS.SysUserSinger.Where(o => o.Id == battle.SingerId2).Select(o => o.SingerPhoto).ToList().FirstOrDefault();
                var obectlist = new
                {
                    Id = battle.Id,
                    SingerId1 = battle.SingerId1,
                    SIngerId2 = battle.SingerId2,
                    SingerId1Phone = "https://localhost:7211/" + sysUserSinger,
                    SingerId2Phone = "https://localhost:7211/" + sysUserSingers,
                    Status = battle.Status,


                };
                list.Add(obectlist);

            }
            return list;
        }

        public bool Add(int singerId, int Uid)
        {   
            var round = _Round.Getallround().Select(o => new { Id = o.Id, Status = o.Status }).ToList().FindAll(o => o.Status == true);
            var Race = _Race.GetStatus().Select(o => new { Id = o.Id }).ToList();
            Vote vote = _db.LMS.Vote.Where(o => o.SingerId == singerId && o.Status == true && o.RoundId == round[0].Id).FirstOrDefault();
            if (vote != null)
            {
                Vote vote2 = _db.LMS.Vote.Where(o => o.SingerId == singerId).FirstOrDefault();
                vote2.Count = vote2.Count + 1;
                _db.LMS.Vote.Update(vote2);
                _db.LMS.SaveChanges();
                return true;
            }
            else
            {
                Vote vote1 = new Vote();
                vote1.Count = 0;
                vote1.RoundId = round[0].Id;
                vote1.Status = true;
                vote1.SingerId = singerId;
                vote1.RacedId = Race[0].Id;
                _db.LMS.Add(vote1);
                _db.LMS.SaveChanges();
                return true;
            }

        }

        public bool AddScore(int JudgeId, double Sore, int singerId)
        {
            var round = _Round.Getallround().Select(o => new { Id = o.Id, Status = o.Status }).ToList().FindAll(o => o.Status == true);
            var Race = _Race.GetStatus().Select(o => new { Id = o.Id }).ToList();
            Vote vote = _db.LMS.Vote.Where(o => o.SingerId == singerId&&o.Status == true && o.RoundId == round[0].Id).FirstOrDefault();
            if (vote != null)
            {
                Vote vote2 = _db.LMS.Vote.Where(o => o.SingerId == singerId).FirstOrDefault();
                vote2.Score = Sore;
                vote2.JudgeId= JudgeId;
                _db.LMS.Vote.Update(vote);
                _db.LMS.SaveChanges();
                return true;
            }
            else
            {
                Vote vote1=new Vote();
                vote1.Score = Sore;
                vote1.Count = 0;
                vote1.JudgeId = JudgeId;
                vote1.RoundId = round[0].Id;
                vote1.Status = true;
                vote1.SingerId = singerId;
                vote1.RacedId = Race[0].Id;
                _db.LMS.Add(vote1);
                _db.LMS.SaveChanges();
                return true;
            }
        }
        public bool UpdateStatus(int Id)
        {
            Vote reuslt = _db.LMS.Vote.Where(o => o.Id == Id).FirstOrDefault();
            reuslt.Status = false;
            _db.LMS.Update(reuslt);
            _db.LMS.SaveChanges();
            return true;
        }
        public IQueryable<Vote> GetAll()
        {
            var requery=from o in _db.LMS.Vote select o;
            return requery;
        }
       public IQueryable<Vote> voteSort()
        {
            var query=_db.LMS.Vote.Where(o=>o.Status==true).OrderByDescending(e => e.EndScore);
            return query;
        }

       public  List<object> Voteshowsinger()//前台展示
        {
             List<object> list = new List<object>();
            var Vote=from o in _db.LMS.Vote.Where(o=>o.Status==true) select o;
            if (Vote.ToList().Count > 0)
            {
                foreach (var vote in Vote.ToList())
                {
                    var sysUserSinger = _db.LMS.SysUserSinger.Where(o => o.Id == vote.SingerId).Select(o => new {
                        SingerName=o.SingerName,
                        SingerPhoto=o.SingerPhoto,
                        Sex=o.Sex,
                        Motto=o.Motto,
                    }).ToList().FirstOrDefault();
                    var obectlist = new
                    {
                        Id =vote.Id,
                        SingerName = sysUserSinger.SingerName,
                        SingerPhoto= "https://localhost:7211/"+sysUserSinger.SingerPhoto,
                        Sex=sysUserSinger.Sex,
                        Motto=sysUserSinger.Motto,
                        Count=vote.Count,
                        EndSote=vote.EndScore,

                    };
                    list.Add(obectlist);

                }
            }
            return list;
        }

    }
}
