using CQIE.OnlineVote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQIE.OnlineVote.Services
{
    public interface ISysRoleService
    {
        IQueryable<SysRole> GetoneRole(int Id);//利用Id查用户的角色信息
    }
}
