using CQIE.OnlineVote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQIE.OnlineVote.Services
{
    public interface IMenuRoleService
    {
        IQueryable<MenuRole> GetMenuRoles(int Rid);//根据RID获取角色的菜单;
    }
}
