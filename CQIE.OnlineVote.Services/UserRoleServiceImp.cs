using CQIE.OnlineVote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQIE.OnlineVote.Services
{
    public class UserRoleServiceImp:IUserRoleService
    {
        private readonly CQIE.OnlineVote.DBManager.IDbManager m_db;
        public UserRoleServiceImp(CQIE.OnlineVote.DBManager.IDbManager dbManager)
        {
                m_db = dbManager;
        }
      
    }
}
