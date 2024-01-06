using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CQIE.OnlineVote.Models;
namespace CQIE.OnlineVote.Services
{
    public class BattleUserServiceImp:IBattleUserService
    {
        private readonly CQIE.OnlineVote.DBManager.IDbManager _db;
        public BattleUserServiceImp(CQIE.OnlineVote.DBManager.IDbManager dbManager)
        {
            _db = dbManager;
        }
      public BattleUser GetUserId(int UserId,int BattleId)
        {

          var requey = _db.LMS.Temp.Where(o => o.UserId == UserId && o.BattleId == BattleId).FirstOrDefault();
          return requey;
        
        }//判断用户是否投
        public BattleUser Add(int UserId, int BattleId, int singerId)
        {

            BattleUser user=new BattleUser();
            user.UserId = UserId;
            user.BattleId = BattleId;
            user.SingerId = singerId;
            _db.LMS.Temp.Add(user);
            _db.LMS.SaveChanges();
            return user;

        }
   

    }
}
