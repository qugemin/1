using CQIE.OnlineVote.DBManager;
using CQIE.OnlineVote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQIE.OnlineVote.Services
{
    public class CompetitionRoundServiceImp:ICompetitonRoundService
    {
        private readonly CQIE.OnlineVote.DBManager.IDbManager _dbManager;
        public CompetitionRoundServiceImp(CQIE.OnlineVote.DBManager.IDbManager dbManager)
        {
            _dbManager = dbManager;
      
        }

        public IQueryable<CompetitionRound> Getallround() { 
        
             var query=from o in _dbManager.LMS.CompetitionRound select o;
            return query;
        }//查询所有轮次

        public IQueryable<CompetitionRound> GetId(int Id) { 
            var query=from o in _dbManager.LMS.CompetitionRound.Where(o=>o.Id==Id) select o;
            return query;     
        
        }//查Id改信息

       public IQueryable<CompetitionRound> GetRoundName(string RounName)
        {
            if (RounName == null)
            {
                var query = from o in _dbManager.LMS.CompetitionRound select o;
                return query;

            }
            var query1=from o in _dbManager.LMS.CompetitionRound.Where(o=>o.RoundName.Contains(RounName)) select o;
            return query1;
        }
        public bool Add(string RoundName, int CompetitionsId){
            var ss = from o in _dbManager.LMS.CompetitionRound.Where(o=>o.Status==true) select o;
            if (ss.ToList().Count > 0)
            {
                return false;
            }
            if (CompetitionsId == 0||RoundName=="")
            {
                return false;
            }
           CompetitionRound competitionRound = new CompetitionRound();
            competitionRound.RoundName = RoundName;
            competitionRound.CompetitionsId = CompetitionsId;
            competitionRound.Status = true;
            _dbManager.LMS.Add(competitionRound);
            _dbManager.LMS.SaveChanges();
            return true;
           
        }//添加新的轮次

       public  bool updateStatus(int Id) {
            CompetitionRound competition = _dbManager.LMS.CompetitionRound.Where(o => o.Id == Id).FirstOrDefault();
            competition.Status = false;
           _dbManager.LMS.CompetitionRound.Update(competition);
           _dbManager.LMS.SaveChanges();                                
            return true;
           

           


        }//修改状态

        public bool updateRound(int Id, string RoundName, int CompetitionsId)
        {

            if (RoundName == "")
            {
                return false;
            }
            CompetitionRound query = _dbManager.LMS.CompetitionRound.Where(o => o.Id == Id).FirstOrDefault();
            query.RoundName= RoundName;
            query.CompetitionsId= CompetitionsId;
            _dbManager.LMS.Update(query);
            _dbManager.LMS.SaveChanges();

            return true;

        }//修改比赛的轮次的详情信息
    }
}
