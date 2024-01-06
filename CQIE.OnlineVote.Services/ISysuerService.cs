using CQIE.OnlineVote.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Globalization;
namespace CQIE.OnlineVote.Services
{
    public interface ISysuerService
    {
         Sysuser Login(string Account, string Password);
        bool register(string Account, string Password,string Phone);

        IQueryable<Sysuser> GetALL();//获取的全部信息
        IQueryable<Sysuser> GettId(int Id);
        IQueryable<Sysuser> GetSysuer(string Account);//查账号

        bool UpdateStatus(int Id);//修改角色的状态

        bool Update(int Id,string Account, string Password);
 
    }
}
