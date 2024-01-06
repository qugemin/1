using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CQIE.OnlineVote.Models;

namespace CQIE.OnlineVote.Services
{
    public interface IVoteService
    {
        Vote GetsingerId(int SingerId,int roundid);

        bool Add(int singerId, int Uid);

        bool UpdateStatus(int Id);//修改用户的状态；
        List<object> UerVote();

        List<object> Voteshowsinger();
        IQueryable<Vote> voteSort();

        bool AddScore(int JudgeId, double Sore, int singerId);

       IQueryable< Vote> GetRoundId(int Id);
        IQueryable<Vote> GetAll();
    }
}
