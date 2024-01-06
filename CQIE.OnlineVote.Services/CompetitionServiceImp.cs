using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CQIE.OnlineVote.Models;
using System.Threading.Tasks;

namespace CQIE.OnlineVote.Services
{
    public class CompetitionServiceImp : ICompetitionService
    {
        private readonly CQIE.OnlineVote.DBManager.IDbManager _dbManage;
        public CompetitionServiceImp(CQIE.OnlineVote.DBManager.IDbManager dbManager)
        {
            _dbManage = dbManager;
        }
        public bool AddCompetition(string ThemeName, string Described, DateTime StartTime, DateTime EndTime)
        {
            Competition competition = new Competition();
            competition.ThemeName = ThemeName;
            competition.Describe = Described;
            competition.Status = false;
            competition.StartTime = StartTime;
            competition.EndTime = EndTime;
            _dbManage.LMS.Competition.Add(competition);
            _dbManage.LMS.SaveChanges();
            return true;
        }
        public IQueryable<Competition> GetALLcompetition()
        {
           
                var query = from o in _dbManage.LMS.Competition select o;
                return query;
          
        }
        public IQueryable<Competition> GetStatus()
        {
            var query=from o in _dbManage.LMS.Competition.Where(o=>o.Status==true) select o;
            return query;
        }

        public bool UpdateStatus(int id)//修改比赛的状态
        {
             Competition competition=_dbManage.LMS.Competition.Where(o=>o.Id==id).FirstOrDefault();
             var query = from o in _dbManage.LMS.Competition.Where(o=>o.Status==true) select o;
            if (query.ToList().Count==0&&competition.Status==false)
            {
                if (competition.Status == true)
                {
                    competition.Status = false;
                }
                else
                {
                    competition.Status=true;
                }
                _dbManage.LMS.Competition.Update(competition);
                _dbManage.LMS.SaveChanges();
                return true;
            }
            else if(competition.Status == true)
            {
                competition.Status = false;
                _dbManage.LMS.Competition.Update(competition);
                _dbManage.LMS.SaveChanges();
                return true;
            }
         
            return false;
                
         }
        public IQueryable<Competition> GetId(int Id)
        {
            var query=from o in _dbManage.LMS.Competition.Where(o=>o.Id==Id) select o;
            return query;
        }
        public IQueryable<Competition> GetThemName(string ThemeName)
        {
            if (ThemeName == null)
            {
                var query1 = from o in _dbManage.LMS.Competition select o;
                return query1;

            }
            var query=from o in _dbManage.LMS.Competition.Where(o => o.ThemeName.Contains(ThemeName)) select o;
            return query;
        }
        public bool UpdateCompetion(int Id, string ThemeName, string Describe, DateTime StartTime, DateTime EndTime)
        {
            Competition competition = _dbManage.LMS.Competition.Where(o => o.Id == Id).FirstOrDefault();
            if (competition == null)
            {
                return false;
            }
            competition.ThemeName  = ThemeName;
            competition.Describe = Describe;
            competition.StartTime = StartTime;
            competition.EndTime = EndTime;
            _dbManage.LMS.Competition.Update(competition);
            _dbManage.LMS.SaveChanges();
            return true;
        }
    }
}
