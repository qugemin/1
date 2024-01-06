using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using OfficeOpenXml;
using Org.BouncyCastle.Utilities.Encoders;
namespace CQIE.OnlineVote.Manager.Controllers
{
    [Route("[controller]/[action]")]
    [EnableCors("any")]

    public class SysuserSingerController : Controller
    {
        private readonly CQIE.OnlineVote.Services.ISysUserSingerService _SingerUser;
        public SysuserSingerController(CQIE.OnlineVote.Services.ISysUserSingerService sysUserSingerService)
        {
                    _SingerUser = sysUserSingerService;
        }
        [HttpGet]
        public IActionResult GetALLSinger()
        {

            var Singer = _SingerUser.GetSysUserSingers().Select(o => new
            {
               id=o.Id,
               singerName=o.SingerName,
               singerPhoto= "https://localhost:7211/"+o.SingerPhoto,
               singerAge =o.SingerAge,
               sex=o.Sex,
               singerDescribe=o.SingerDescribe,
               motto=o.Motto,
               competitionsI=o.CompetitionsId,
               status=o.Status
           
            });
           return new JsonResult(Singer);
        }
        [HttpGet]
        public IActionResult GetId(int Id)
        {

            var Singer = _SingerUser.GetId(Id).Select(o => new
            {
                id = o.Id,
                singerName = o.SingerName,
                singerPhoto = "https://localhost:7211" + o.SingerPhoto,
                singerAge = o.SingerAge,
                sex = o.Sex,
                singerDescribe = o.SingerDescribe,
                motto = o.Motto,
                competitionsI = o.CompetitionsId,
                status = o.Status
            });
            return new JsonResult(Singer);
        }
        [HttpGet]
        public IActionResult GetStatus()
        {
            var reslut = _SingerUser.GetSysUserSingers().Select(o => new
            {
                id = o.Id,
                singerName = o.SingerName,
                singerPhoto = "https://localhost:7211/" + o.SingerPhoto,
                singerAge = o.SingerAge,
                sex = o.Sex,
                singerDescribe = o.SingerDescribe,
                motto = o.Motto,
                competitionsI = o.CompetitionsId,
                status = o.Status
            }).ToList().FindAll(o => o.status == true);
            return new JsonResult(reslut);
        }
        [HttpGet]
        public IActionResult GetsysuerName(string SingerName)
        {
            var result = _SingerUser.GetSysuerName(SingerName).Select(o => new
            {
                id = o.Id,
                singerName = o.SingerName,
                singerPhoto = "https://localhost:7211/" + o.SingerPhoto,
                singerAge = o.SingerAge,
                sex = o.Sex,
                singerDescribe = o.SingerDescribe,
                motto = o.Motto,
                competitionsI = o.CompetitionsId,
                status = o.Status
            });
            return new JsonResult(result);
        }
        public record singerAdd(
            IFormFile FormFile
            );
        [HttpPost]
        public async Task<IActionResult> Addsinger([FromForm] singerAdd singerAdd)
        {

           bool judet = _SingerUser.Add(singerAdd.FormFile);
            if (judet == true)
            {
                return Ok("添加成功");
            }
            return Ok("添加失败");
        }
       public record Singgerstatus
        (
            int Id
        );
        [HttpPut]
        public IActionResult UpdateStadu([FromBody] Singgerstatus singgerstatus)
        {
             bool judge=_SingerUser.updateStatus(singgerstatus.Id);
            if (judge == true)
            {
                return Ok("修改成功");
            }
            return Ok("修改失败");
        }
        public record UpdateSinger(
           int Id, string SingerNamem, IFormFile SingerPhoto, string SingerAge, bool Sex, string SingerDescribe, string Motto, int CompetitionsId
            );
        [HttpPut]
        public IActionResult UpdateSingetuer([FromForm] UpdateSinger u)
        {
            bool judget=_SingerUser.updateInformation(u.Id,u.SingerNamem,u.SingerPhoto,u.SingerAge,u.Sex,u.SingerDescribe,u.Motto,u.CompetitionsId);
            if (judget == true)
            {
                return Ok("修改成功");
            }
            return Ok("修改失败");
        }


    }
}
