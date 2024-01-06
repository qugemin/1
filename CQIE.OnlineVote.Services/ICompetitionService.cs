using CQIE.OnlineVote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQIE.OnlineVote.Services
{
    public interface ICompetitionService
    {
        bool AddCompetition(string ThemeName, string Described, DateTime StartTime, DateTime EndTime);//添加一个比赛的信息
        IQueryable<Competition> GetALLcompetition();//查询所有的比赛信息
        IQueryable<Competition> GetStatus();
        bool UpdateStatus(int id);//修改状态

        IQueryable<Competition> GetId(int Id);

        IQueryable<Competition> GetThemName(string ThemeName);

        bool UpdateCompetion(int Id,  string ThemeName, string Describe, DateTime StartTime, DateTime EndTime);




    }
}
