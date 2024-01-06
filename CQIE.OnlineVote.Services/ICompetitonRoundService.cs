using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CQIE.OnlineVote.Models;

namespace CQIE.OnlineVote.Services
{
    public interface ICompetitonRoundService
    {
        IQueryable<CompetitionRound> Getallround();//查询所有轮次

        IQueryable<CompetitionRound> GetId(int Id);//查Id改信息

        IQueryable<CompetitionRound> GetRoundName(string RounName);

        bool Add(string RoundName, int CompetitionsId);//添加新的轮次

        
        bool updateStatus(int Id);//修改状态

        bool updateRound(int Id, string RoundName, int CompetitionsId);//修改比赛的轮次的详情信息

    }
}
