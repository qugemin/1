using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using CQIE.OnlineVote.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using OfficeOpenXml;

namespace CQIE.OnlineVote.Services
{
    public interface ISysUserSingerService
    {
        IQueryable<SysUserSinger> GetSysUserSingers();

        IQueryable<SysUserSinger> GetId(int Id);


        IQueryable<SysUserSinger> GetSysuerName(string SingerName);//查找歌手

        bool Add(IFormFile formFile);

        bool updateStatus(int Id);
        bool updateInformation(int Id, string SingerNamem, IFormFile SingerPhoto, string SingerAge, bool Sex, string SingerDescribe, string Motto, int CompetitionsId);
    }
}
