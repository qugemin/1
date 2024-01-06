using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CQIE.OnlineVote.Models;

namespace CQIE.OnlineVote.Services
{
    public interface IBattleService
    {
        IQueryable<Battle> GetBattleList();

        List<object> BattleLists();

        string GetAdd(int SingerId2,int SingerId1);

        bool Update(int Id);

    }
}
