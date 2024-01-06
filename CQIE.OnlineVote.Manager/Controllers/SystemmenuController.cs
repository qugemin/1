using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CQIE.OnlineVote.Manager.Controllers
{
    [Route("[controller]/[action]")]
    [EnableCors("any")]

    public class SystemmenuController : Controller
    {
        private readonly CQIE.OnlineVote.Services.ISystemMenuService _systemMenuService;
        public SystemmenuController(CQIE.OnlineVote.Services.ISystemMenuService systemMenuService)
        {
            _systemMenuService = systemMenuService;
        }
        [HttpGet]
        public IActionResult SysmenGetALL()
        {
            var result = _systemMenuService.GetSysmenRole().Select(o => new
            {
                o.Id,
                o.MenuName
            }).ToList();
            return new JsonResult(result);
        }
    }
}
