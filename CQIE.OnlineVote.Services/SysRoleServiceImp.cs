using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CQIE.OnlineVote.Models;
namespace CQIE.OnlineVote.Services
{
    public class SysRoleServiceImp:ISysRoleService
    {
        private readonly CQIE.OnlineVote.DBManager.IDbManager m_dbManager;
        public SysRoleServiceImp(CQIE.OnlineVote.DBManager.IDbManager dbManager)
        {
                m_dbManager = dbManager;
        }
       public IQueryable<SysRole> GetoneRole(int Id)
        {
            var query=from o in m_dbManager.LMS.SysRole 
                      join o1 in m_dbManager.LMS.USerRole on o.Id equals o1.RId
                      where o1.UId == Id select o;
            return query;
        }
    }
}
