using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CQIE.OnlineVote.Models;
using Microsoft.EntityFrameworkCore;

namespace CQIE.OnlineVote.Services
{
    public class SystemMenuServiceImp:ISystemMenuService
    {
        private readonly CQIE.OnlineVote.DBManager.IDbManager m_dbManager;
        public SystemMenuServiceImp(CQIE.OnlineVote.DBManager.IDbManager dbManager)
        {
                  m_dbManager = dbManager;
        }
        public IQueryable<SystemMenu> GetSysmenRole()
        {
            var query = from o in m_dbManager.LMS.SystemMenu.Include(c => c.SubMenus)
                        where o.ParentID == null
                        select o;
            return query;
        }
        public IQueryable<SystemMenu> GetCaidan(int PartId)
        {
             var query=from o in m_dbManager.LMS.SystemMenu where o.ParentID == PartId select o;
            return query;
        }
    }
}
