using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CQIE.OnlineVote.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
namespace CQIE.OnlineVote.Services
{
    public class SysuerServiceImp : ISysuerService
    {
        private readonly CQIE.OnlineVote.DBManager.IDbManager m_dbManager;
        public SysuerServiceImp(CQIE.OnlineVote.DBManager.IDbManager dbManager)
        {
            m_dbManager = dbManager;

        }

        public Sysuser Login(string Account, string Password)
        {
            var query = m_dbManager.LMS.Sysuser.Where(o => o.Account == Account && o.Password == Password).FirstOrDefault();
            return query;

        }
        public bool register(string Account, string Password, string Phone)
        {   
            bool judget = true;
            var sysuerd=from o in m_dbManager.LMS.Sysuser.Where(o=>o.Account==Account) select o;
            if (sysuerd.ToList().Count > 0)
            {
                return false;
            }
            if (Account == null || Password.Length < 6 || Phone == null)
            {
                judget = false;
                return judget;
            }
            Sysuser sysuser = new Sysuser();
            sysuser.Account = Account;
            sysuser.Password = Password;
            sysuser.Status = true;
            SysuerInformation sysuerInformation = new SysuerInformation();
            sysuerInformation.Phone = Phone;
            USerRole uSerRole = new USerRole();
            uSerRole.RId = 2;
            sysuser.SyserInfos = sysuerInformation;
            uSerRole.Sysusers = sysuser;
            m_dbManager.LMS.AddRange(sysuser, uSerRole);
            m_dbManager.LMS.SaveChanges();
            return judget;

        }
        public IQueryable<Sysuser> GetALL()
        {
            var queey = from o in m_dbManager.LMS.Sysuser select o;
            return queey;
        }
        public bool UpdateStatus(int Id)//修改状态
        {
            Sysuser sysuser = m_dbManager.LMS.Sysuser.Where(o => o.Id == Id).FirstOrDefault();
            if (sysuser != null)
            {
                if (sysuser.Status != false)
                {
                    sysuser.Status = false;
                }
                else
                {
                    sysuser.Status = true;
                }
                m_dbManager.LMS.Sysuser.Update(sysuser);
                m_dbManager.LMS.SaveChanges();
                return true;
            }
            return false;
        }
        public IQueryable<Sysuser> GetSysuer(string Account)//查账号
        {
            if (Account == null)
            {
                var queey = from o in m_dbManager.LMS.Sysuser select o;
                return queey;
            }
            var query1 = from o in m_dbManager.LMS.Sysuser.Where(o => o.Account.Contains(Account)) select o;
            return query1;
        }
        public bool Update(int Id, string Account, string Password)
        {
            if (Id == null||Password.Length<6)
            {
                return false;
            }
            Sysuser sysuser = m_dbManager.LMS.Sysuser.Where(o => o.Id == Id).FirstOrDefault();
            sysuser.Account = Account;
            sysuser.Password = Password;
            m_dbManager.LMS.Update(sysuser);
            m_dbManager.LMS.SaveChanges();
            return true;
        }
        public IQueryable<Sysuser> GettId(int Id)
        {
            var query=from o in m_dbManager.LMS.Sysuser.Where((o)=>o.Id == Id)  select o;
            return query;
        }
    }
}
