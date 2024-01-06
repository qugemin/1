using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using CQIE.OnlineVote.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Identity.Client;
namespace CQIE.OnlineVote.Manager.Controllers
{
    [Route("[controller]/[action]")]
    [EnableCors("any")]

    public class CompetitionController : Controller
    {
        private readonly CQIE.OnlineVote.Services.ICompetitionService _competitionService;
        public CompetitionController(CQIE.OnlineVote.Services.ICompetitionService competitionService)
        {
            _competitionService = competitionService;
        }
        public record Competitions(
             string ThemeName,
             string Describe,
             DateTime StartTime,
             DateTime EndTime
            );
        [HttpPost]
        public IActionResult competitionAdd([FromBody] Competitions competitions)
        {
            bool judget = _competitionService.AddCompetition(competitions.ThemeName, competitions.Describe, competitions.StartTime, competitions.EndTime);
            return Ok("成功添加新的比赛信息");
        }
        [HttpGet]
        public IActionResult competitionAGetALL()
        {
            var result = _competitionService.GetALLcompetition();
            return new JsonResult(result);
        }
        [HttpGet]
        public IActionResult compettionGetStatus()
        {
            var result=_competitionService.GetStatus();
            return new JsonResult(result);
        }
        [HttpGet]
         public IActionResult GetId(int Id)
        {
            var result=_competitionService.GetId(Id);
            return new JsonResult(result);
        }
        [HttpGet]
        public IActionResult GetThemss(string ThemeName)
        {
            var result = _competitionService.GetThemName(ThemeName);
            return new JsonResult(result);
        }
        public record Competitionsupdate(int Id);
        [HttpPut]
        public IActionResult competitionUpdate([FromBody]Competitionsupdate competitionsupdate)
        {
            var result=_competitionService.UpdateStatus(competitionsupdate.Id);
            if (result == true)
            {
                return Ok("修改状态成功");
            }
            return Ok("修改失败，还有比赛在进行中");
        }
        public record Competitionupdate(
           int Id,
          string ThemeName,
          string Describe,
          DateTime StartTime,
          DateTime EndTime
         );
        [HttpPut]
        public IActionResult update([FromBody] Competitionupdate change)
        {
            bool juedget = _competitionService.UpdateCompetion(change.Id, change.ThemeName, change.Describe, change.StartTime, change.EndTime);
            if (juedget == true)
            {
                return Ok("修改成功");
            }
            return Ok("修改失败");
        }

    }
}
