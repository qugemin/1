using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Formats.Asn1;
using CQIE.OnlineVote.Models;
using System.Data;

namespace CQIE.OnlineVote.Manager.Controllers
{
    [Route("[controller]/[action]")]
    public class AdminController: Controller
    {
        private static System.Text.StringBuilder m_Resp = new System.Text.StringBuilder();
        [HttpPost]
        public IActionResult CreateDB([FromServices] CQIE.OnlineVote.DBManager.IDbManager dbManager)
        {
            m_Resp.AppendLine("开始创建数据库!!!!");
            dbManager.LMS.Database.EnsureDeleted();
            dbManager.LMS.Database.EnsureCreated();
            m_Resp.AppendLine("初始化数据库成功！！！！！！！！");

            #region //真个用户角色的初始化
            m_Resp.AppendLine("初始化系统的账户");
            var admin = new CQIE.OnlineVote.Models.Sysuser()
            {
                Account = "admin",
                Password = "123456",
                Status = true,
            };
            var Judge = new CQIE.OnlineVote.Models.Sysuser()
            {
                Account = "Judge",
                Password = "123456",
                Status = true,
            };
            var Judge1 = new CQIE.OnlineVote.Models.Sysuser()
            {
                Account = "Judge1",
                Password = "123456",
                Status = true,
            };
            dbManager.LMS.Sysuser.AddRange(admin, Judge, Judge1);
            dbManager.LMS.SaveChanges();
            m_Resp.AppendLine("初始化完成");
            m_Resp.AppendLine("初始化用户表表和个人信息");
            var admininformation = new CQIE.OnlineVote.Models.SysuerInformation()
            {
                Phone = "13368237958",
                Sysusers = admin,
            };
            var Judgeinformation = new CQIE.OnlineVote.Models.SysuerInformation()
            {
                Phone = "13368237958",
                Sysusers = Judge,
            };
            var Judge1information = new CQIE.OnlineVote.Models.SysuerInformation()
            {
                Phone = "13368237958",
                Sysusers = Judge1,
            };
            dbManager.LMS.AddRange(admininformation, Judgeinformation, Judge1information);
            dbManager.LMS.SaveChanges();
            m_Resp.AppendLine("初始化完成");
            m_Resp.AppendLine("初始化角色");
            var Role = new CQIE.OnlineVote.Models.SysRole() {
                RoleName = "管理员",
                Describe = "最高权限，对后台管理",
            };
            var Role1 = new CQIE.OnlineVote.Models.SysRole()
            {
                RoleName = "用户",
                Describe = "在前台对歌手投票",
            };
            var Role2 = new CQIE.OnlineVote.Models.SysRole()
            {
                RoleName = "评委",
                Describe = "对歌手进行打分",
            };
            dbManager.LMS.SysRole.AddRange(Role, Role1, Role2);
            dbManager.LMS.SaveChanges();
            m_Resp.AppendLine("初始化完成");
            m_Resp.AppendLine("初始化角色和用户的关系");
            dbManager.LMS.Set<CQIE.OnlineVote.Models.USerRole>().Add(new CQIE.OnlineVote.Models.USerRole
            {
                Sysusers = admin,
                Roles = Role,

            });
            dbManager.LMS.Set<CQIE.OnlineVote.Models.USerRole>().Add(new CQIE.OnlineVote.Models.USerRole
            {
                Sysusers = Judge,
                Roles = Role2,

            });
            dbManager.LMS.Set<CQIE.OnlineVote.Models.USerRole>().Add(new CQIE.OnlineVote.Models.USerRole
            {
                Sysusers = Judge1,
                Roles = Role2,

            });
            dbManager.LMS.SaveChanges();
            m_Resp.AppendLine("初始化完成");
            #endregion
            m_Resp.AppendLine("初始化菜单列表");
            var adminsysmen = new CQIE.OnlineVote.Models.SystemMenu() {
                MenuName = "系统管理",
                Url = "",
            };
            var usersysmen = new CQIE.OnlineVote.Models.SystemMenu()
            {
                MenuName = "用户管理",
                Url = "",
            };
            var judgetsysmen = new CQIE.OnlineVote.Models.SystemMenu()
            {
                MenuName = "评委管理",
                Url = "",
            };
            dbManager.LMS.SystemMenu.AddRange(adminsysmen, usersysmen, judgetsysmen);
            dbManager.LMS.SaveChanges();
            m_Resp.AppendLine("初始化主菜单完成");
            m_Resp.AppendLine("初始化子菜单");



            var admintuichu = new CQIE.OnlineVote.Models.SystemMenu() { 
                 MenuName="退出",
                 Url= "/UnUserLogin",
            };
         
            var adminbvote = new CQIE.OnlineVote.Models.SystemMenu()
            {
                MenuName = "投票详情",
                Url = "/AdminVote",
            };
            var adminbattle = new CQIE.OnlineVote.Models.SystemMenu()
            {
                MenuName = "对战",
                Url = "/AdminBattle",
            };
            var admincomptitonround = new CQIE.OnlineVote.Models.SystemMenu()
            {
                MenuName = "比赛轮次",
                Url = "/AdminRund",
            };
            var admincomptiton = new CQIE.OnlineVote.Models.SystemMenu()
            {
                MenuName = "比赛信息",
                Url = "/AdminRacedss",
            };
            var adminsinger = new CQIE.OnlineVote.Models.SystemMenu()
            {
                MenuName = "歌手信息",
                Url = "/AdminSinger",
            };
            var adminUser = new CQIE.OnlineVote.Models.SystemMenu()
            {
                MenuName = "用户信息",
                Url = "/AdminUser",
            };
            #region 除了管理员以外的菜单
            var judgetscore = new CQIE.OnlineVote.Models.SystemMenu()
            {
                MenuName = "评分",
                Url = "",
            };
            var usersinger = new CQIE.OnlineVote.Models.SystemMenu()
            {
                MenuName = "歌手",
                Url = "/SingerUser",
            };
            var usercompition = new CQIE.OnlineVote.Models.SystemMenu()
            {
                MenuName = "首页",
                Url = "/HomeIndex",
            };
            var judgetcompition = new CQIE.OnlineVote.Models.SystemMenu()
            {
                MenuName = "首页",
                Url = "/HomeIndex",
            };
            var userballte = new CQIE.OnlineVote.Models.SystemMenu()
            {
                MenuName = "对战投票",
                Url = "/UserBattleVote",
            };
            var uservote = new CQIE.OnlineVote.Models.SystemMenu()
            {
                MenuName = "结果",
                Url = "/MyOutcome",
            };
            var judgetsinger = new CQIE.OnlineVote.Models.SystemMenu()
            {
                MenuName = "歌手",
                Url = "/SingerUser",
            };
            var judgebattle = new CQIE.OnlineVote.Models.SystemMenu()
            {
                MenuName = "对战打分",
                Url = "/JudgeBattleSore",
            };
            var judgevote = new CQIE.OnlineVote.Models.SystemMenu()
            {
                MenuName = "结果",
                Url = "/MyOutcome",
            };
            var judgetuichu = new CQIE.OnlineVote.Models.SystemMenu()
            {
                MenuName = "退出",
                Url = "/UnUserLogin",
            };
            var usertuichu= new CQIE.OnlineVote.Models.SystemMenu()
            {
                MenuName = "退出",
                Url = "/UnUserLogin",
            };
            #endregion


            adminsysmen.SubMenus.Add(admintuichu);
            adminsysmen.SubMenus.Add(adminbvote);
            adminsysmen.SubMenus.Add(adminbattle);
            adminsysmen.SubMenus.Add(admincomptitonround);
            adminsysmen.SubMenus.Add(admincomptiton);
            adminsysmen.SubMenus.Add(adminUser);
            adminsysmen.SubMenus.Add(adminsinger);

            usersysmen.SubMenus.Add(usertuichu);
            usersysmen.SubMenus.Add(uservote);
            usersysmen.SubMenus.Add(userballte);
            usersysmen.SubMenus.Add(usersinger);
            usersysmen.SubMenus.Add(usercompition);

            judgetsysmen.SubMenus.Add(judgetuichu);
            judgetsysmen.SubMenus.Add(judgevote);
            judgetsysmen.SubMenus.Add(judgebattle);
            judgetsysmen.SubMenus.Add(judgetsinger);
            judgetsysmen.SubMenus.Add(judgetcompition);
            m_Resp.AppendLine("初始化完成");
            m_Resp.AppendLine("初始化菜单和角色的关系");
            dbManager.LMS.Set<CQIE.OnlineVote.Models.MenuRole>().Add(new CQIE.OnlineVote.Models.MenuRole() { 
            
              Roels= Role,
              Menus=adminsysmen,
            });
            dbManager.LMS.Set<CQIE.OnlineVote.Models.MenuRole>().Add(new CQIE.OnlineVote.Models.MenuRole()
            {

                Roels = Role1,
                Menus = usersysmen,
            });
            dbManager.LMS.Set<CQIE.OnlineVote.Models.MenuRole>().Add(new CQIE.OnlineVote.Models.MenuRole()
            {

                Roels = Role2,
                Menus = judgetsysmen,
            });
            dbManager.LMS.SaveChanges();
            m_Resp.AppendLine("初始化完成");

            return Content(m_Resp.ToString());
        }
        
    }
}
