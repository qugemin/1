using CQIE.OnlineVote.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CQIE.OnlineVote.Manager.Controllers
{

    [Route("[controller]/[action]")]
    [EnableCors("any")]

    public class RaceRoundController : Controller
    {
        private readonly CQIE.OnlineVote.Services.ICompetitonRoundService _competitonRoundService;
        private readonly CQIE.OnlineVote.Services.IVoteService _vote;
        public RaceRoundController(CQIE.OnlineVote.Services.ICompetitonRoundService competitonRoundService,CQIE.OnlineVote.Services.IVoteService vote)
        {
            _competitonRoundService = competitonRoundService;
            _vote = vote;
        }
        [HttpGet]
        public IActionResult GetALLRound()
        {
            var result=_competitonRoundService.Getallround();
            return new JsonResult(result);
        }
        [HttpGet]
        public IActionResult GupdateStatus(int Id)
        {
            bool judget=_competitonRoundService.updateStatus(Id);
            if (judget == true)
            {
                var vote = _vote.GetRoundId(Id);
                
                return Ok("修改状态成功");
            }
            return Ok("还有一轮本赛正在进行");
        }
        [HttpGet]
        public IActionResult GetId(int Id)
        {
            var result=_competitonRoundService.GetId(Id);
            return new JsonResult(result);
        }
        [HttpGet]
        public IActionResult Getroundname(string RoundName)
        {
            var result=_competitonRoundService.GetRoundName(RoundName);
            return new JsonResult(result);
        }
       public record Addround(
           string RoundName, int CompetitionsId
           );
        [HttpPost]
        public IActionResult Add([FromBody]Addround addround) {
            {
                if (addround == null)
                {
                    return Ok("请先添加比赛");
                }
                bool judget = _competitonRoundService.Add(addround.RoundName, addround.CompetitionsId);
                if (judget == true)
                {
                    return Ok("添加成功");
                }
                return Ok("你还有一轮本赛在进行");

            }
        }
        public record Updateround( int Id,
         string RoundName, int CompetitionsId
          );
        [HttpPut]
        public IActionResult UpdateRound([FromBody]Updateround updateround)
        {
            bool judge = _competitonRoundService.updateRound(updateround.Id, updateround.RoundName, updateround.CompetitionsId);
            if (judge == true)
            {
                return Ok("修改成功");
            }
            return Ok("修改失败");
        }
       
    }
}
