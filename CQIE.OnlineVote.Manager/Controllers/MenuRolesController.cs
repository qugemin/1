using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CQIE.OnlineVote.Manager.Controllers
{
    [Route("[controller]/[action]")]
    [EnableCors("any")]
    public class MenuRolesController : Controller
    {
        private readonly CQIE.OnlineVote.Services.IMenuRoleService _MenuRole;
        private readonly CQIE.OnlineVote.Services.ISystemMenuService _systemMenuService;
        public MenuRolesController(CQIE.OnlineVote.Services.IMenuRoleService MenuRole, CQIE.OnlineVote.Services.ISystemMenuService systemMenuService)
        {
            _systemMenuService = systemMenuService;
            _MenuRole = MenuRole;
        }
        [HttpGet]
        public IActionResult GetUserRole(int Rid)
        {
            var result = _MenuRole.GetMenuRoles(Rid);
            if (result != null)
            {
               
                foreach (var role in result)
                {
                    var result1 = _systemMenuService.GetCaidan(role.RoleId);
                    return new JsonResult(result1);
                }
            }
            return Ok("无对应角色的菜单");
        }
    }
}
