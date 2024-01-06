using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CQIE.OnlineVote.Models;
using System.Threading.Tasks;

namespace CQIE.OnlineVote.Services
{
    public interface ISystemMenuService
    {
        IQueryable<SystemMenu> GetSysmenRole();//获取菜单

        IQueryable<SystemMenu> GetCaidan(int PartId);
    }
}
