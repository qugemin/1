using CQIE.OnlineVote.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CQIE.OnlineVote.Manager.Controllers
{
    [Route("[controller]/[action]")]
    [EnableCors("any")]
    public class BattleController : Controller
    {
        private readonly CQIE.OnlineVote.Services.IBattleService _battle;
        public BattleController(CQIE.OnlineVote.Services.IBattleService GetUserId)
        {
            _battle = GetUserId;
        }
        [HttpGet]
        public IActionResult GetBattleList()
        {
            var requey = _battle.GetBattleList();
            return new JsonResult(requey);
        }
        [HttpGet]
        public IActionResult GetBattleListStatus()
        {
            var requey = _battle.GetBattleList().ToList().FindAll(o=>o.Status==true);
            return new JsonResult(requey);
        }
        [HttpGet]
        public IActionResult Getlist()
        {
            List<object> list = _battle.BattleLists();
            return new JsonResult(list);    
        }
        public record Updatebool(
            int Id
            );

        [HttpPut]
        public IActionResult Updates([FromBody]Updatebool U)
        {
             bool judge = _battle.Update(U.Id);
            if (judge == true)
            {
                return Ok("本轮结束");
            }
            return Ok("失败");
        }
        public record Addbattle(
           int SingerId2,
           int SingerId1
           );
        [HttpPost]

        public IActionResult GetAdd([FromBody] Addbattle A)
        {
            string result = _battle.GetAdd(A.SingerId2, A.SingerId1);
            return Ok(result);
        }

   
    }
}
