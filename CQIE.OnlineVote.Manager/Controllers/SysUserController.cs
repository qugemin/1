using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using CQIE.OnlineVote.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using static CQIE.OnlineVote.Manager.Controllers.SysUserController;

namespace CQIE.OnlineVote.Manager.Controllers
{
    [Route("[controller]/[action]")]
    [EnableCors("any")]

    public class SysUserController: Controller
    {
        public CQIE.OnlineVote.Services.ISysuerService _sysuerService;
        public CQIE.OnlineVote.Services.ISysRoleService  _sysroleService;

        public SysUserController(CQIE.OnlineVote.Services.ISysuerService sysuerService,CQIE.OnlineVote.Services.ISysRoleService sysroleService)
        {
            _sysuerService = sysuerService;
            _sysroleService = sysroleService;
        }
        [HttpGet]
        public IActionResult Login(string Account,string Password)
        {
            var result = _sysuerService.Login(Account, Password);
            if (result!=null)
            {
            
                if (result.Status==true)
                {
                    var Role = _sysroleService.GetoneRole(result.Id).ToList();
                    return new JsonResult(new { result, Role});
                }
                return Ok("当前的账号未激活，请联系管理员");
                
            }
             return Ok("密码或账号错误，请重新输入");
        }
        public record Sysuers(string Account,string Password, string Phone);
        [HttpPost]//注册
        public IActionResult register([FromBody]Sysuers sysuers)
        {

            bool judget = _sysuerService.register(sysuers.Account, sysuers.Password, sysuers.Phone);
            if (judget == false)
            {
                return Ok("注册失败");

            }

            return Ok("注册成功");
        }
        [HttpGet]
        public IActionResult GetALL()
        {
            var result = _sysuerService.GetALL();
            return new JsonResult(result);
        }
        public record Sysuersstatus(int Id);
        [HttpPut]
        public IActionResult updateStatusUser([FromBody]Sysuersstatus sysuersstatus)
        {
            var result = _sysuerService.UpdateStatus(sysuersstatus.Id);
            if (result == true)
            {
                return Ok("修改状态成功");
            }
            return Ok("修改状态失败");
        }
        [HttpGet]
        public IActionResult Geisysuer(string Account)
        {
            var result=_sysuerService.GetSysuer(Account);
            return new JsonResult(result);
        }
        public record SysuerUpdate(int Id,string Account,string Password);
        [HttpPut]
        public IActionResult Updated([FromBody]SysuerUpdate sysuerUpdate)
        {
            bool judge = _sysuerService.Update(sysuerUpdate.Id,sysuerUpdate.Account, sysuerUpdate.Password);
            if (judge == true)
            {
                return Ok("修改成功");
            }
            return Ok("修改失败");
        }
        [HttpGet]
        public IActionResult GetId(int Id) { 
            var result=_sysuerService.GettId(Id);
            return new JsonResult(result);
        }
    }
}
