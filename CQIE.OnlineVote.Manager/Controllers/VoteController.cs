using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using CQIE.OnlineVote.Models;

namespace CQIE.OnlineVote.Manager.Controllers
{
    [Route("[controller]/[action]")]
    [EnableCors("any")]

    public class VoteController : Controller
    {
        private readonly CQIE.OnlineVote.Services.IVoteService _service;
        private readonly CQIE.OnlineVote.Services.IBattleService _bt;
        private readonly CQIE.OnlineVote.Services.IBattleUserService _btus;
        public VoteController(CQIE.OnlineVote.Services.IVoteService voteService, CQIE.OnlineVote.Services.IBattleService bt, CQIE.OnlineVote.Services.IBattleUserService btus)
        {
                _service = voteService;
                 _bt = bt;
            _btus = btus;
        }
        public record VoteUser(
            int singerId, int Uid
            );

        [HttpPost]
        public IActionResult Adduser([FromBody] VoteUser A)
        {
            var bt = _bt.GetBattleList().Select(o => new { Id = o.Id, Status = o.Status }).ToList().FindAll(o => o.Status == true);
            var btus = _btus.GetUserId(A.Uid, bt[0].Id);
            if (btus != null)
            {
                return Ok("已经投票");
            }
            bool judget = _service.Add(A.singerId, A.Uid);
            if (judget == true)
            {
                var btss = _bt.GetBattleList().Select(o => new { Id = o.Id, Status = o.Status }).ToList().FindAll(o => o.Status == true);
                var bts = _btus.Add(A.Uid, btss[0].Id,A.singerId);
                return Ok("投票成功");
            }
            return Ok("投票失败");
        }
        public record VoteJudge(
     int JudgeId, double Sore, int singerId
         );

        [HttpPost]
        public IActionResult Addjudge([FromBody]VoteJudge A)
        {
            bool judget = _service.AddScore(A.JudgeId, A.Sore,A.singerId);
            if (judget == true)
            {
                return Ok("打分成功");
            }
            return Ok("已经打分");
        }
        [HttpGet]
        public IActionResult voteUser()
        {
            List<object> list = _service.UerVote();
            return new JsonResult(list);
        }
        [HttpGet]
        public IActionResult voteGelALL()
        {
            var result = _service.GetAll();
            return new JsonResult(result);
        }
        [HttpGet]
        public IActionResult voteSort()
        {
            var result=_service.voteSort();
            return new JsonResult(result);
        }
        [HttpGet]
        public IActionResult showSinger()
        {
            List<object> list=_service.Voteshowsinger();
            return new JsonResult(list);
        }
        public record voteId(
            int Id
            );
        [HttpPut]
        public IActionResult UpdateVote([FromBody]voteId voteId)
        {
            bool judget = _service.UpdateStatus(voteId.Id);
            if (judget == true)
            {
                return Ok("修改成功");
            }
            return Ok("修改失败");
        }
   
    }
}
