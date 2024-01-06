using CQIE.OnlineVote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQIE.OnlineVote.Services
{
    public class MenuRoleServiceImp : IMenuRoleService
    {
        private readonly CQIE.OnlineVote.DBManager.IDbManager _db;
        public MenuRoleServiceImp(CQIE.OnlineVote.DBManager.IDbManager db)
        {
            _db = db;
        }
        public IQueryable<MenuRole> GetMenuRoles(int Rid)//根据RID获取角色的菜单;
        {
           var query=from o in _db.LMS.MenuRole where o.RoleId == Rid select o; 
            return query;
        }
    }
}
