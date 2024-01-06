using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CQIE.OnlineVote.Models;

namespace CQIE.OnlineVote.Services
{
    public interface IBattleUserService
    {
        BattleUser GetUserId(int UserId,int BattleId);

        BattleUser Add(int UserId, int BattleId,int singerId);

    }
}
