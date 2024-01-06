using CQIE.OnlineVote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using System.IO;
using OfficeOpenXml;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml.ConditionalFormatting.Contracts;
namespace CQIE.OnlineVote.Services
{
    public class SysUserSingerServiceImp:ISysUserSingerService
    {
        private readonly CQIE.OnlineVote.DBManager.IDbManager m_dbManager;
        private readonly IHostEnvironment _hostingEnvironment;
        private readonly CQIE.OnlineVote.Services.ICompetitionService _competition;
        public SysUserSingerServiceImp(CQIE.OnlineVote.DBManager.IDbManager dbManager,CQIE.OnlineVote.Services.ICompetitionService competition, IHostEnvironment hostingEnvironment)
        {
            m_dbManager = dbManager;
            _competition = competition; 
            _hostingEnvironment = hostingEnvironment;
        }
        public IQueryable<SysUserSinger> GetSysUserSingers()
        {
            var query = from o in m_dbManager.LMS.SysUserSinger select o;
            return query;
        }
        public IQueryable<SysUserSinger> GetId(int Id)
        {
            var query = from o in m_dbManager.LMS.SysUserSinger.Where(o=>o.Id==Id) select o;
            return query;
        }
       

        public IQueryable<SysUserSinger> GetSysuerName(string SingerName) {
            if (SingerName == null)
            {
                var query1 = from o in m_dbManager.LMS.SysUserSinger select o;
                return query1;

            }
            var query=from o in m_dbManager.LMS.SysUserSinger.Where(o=>o.SingerName.Contains(SingerName)) select o;
            return query;   
        
        }//查找歌手

        public bool updateStatus(int Id)
        {
            SysUserSinger sysUserSinger = m_dbManager.LMS.SysUserSinger.Where(o => o.Id == Id).FirstOrDefault();
            if (sysUserSinger != null)
            {
                if (sysUserSinger.Status == true)
                {
                    sysUserSinger.Status = false;
                }
                else
                {
                    sysUserSinger.Status = true;
                }
                m_dbManager.LMS.SysUserSinger.Update(sysUserSinger);
                m_dbManager.LMS.SaveChanges();
                return true;
            }
           return false;
        }
        private string AddPhoto(IFormFile imageFile)
        {
            string FileName = DateTime.Now.ToString("fffffff") + ".jpg";
            var filePath = Directory.GetCurrentDirectory() + "/wwwroot/images/" + FileName;

            // 保存文件到磁盘  
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                imageFile.CopyTo(stream);
            }
            return FileName;
        }
        public bool updateInformation(int Id, string SingerNamem, IFormFile SingerPhoto, string SingerAge, bool Sex, string SingerDescribe, string Motto, int CompetitionsId)//修改歌手的个人信息
        {
            SysUserSinger singerPhoto = m_dbManager.LMS.SysUserSinger.Where(o => o.Id == Id).FirstOrDefault();
            singerPhoto.SingerName= SingerNamem;
            singerPhoto.SingerAge = SingerAge;
            singerPhoto.Sex = Sex;
            singerPhoto.SingerDescribe = SingerDescribe;
            singerPhoto.CompetitionsId = CompetitionsId;
            singerPhoto.Motto = Motto;
            if (singerPhoto == null)
            {
                m_dbManager.LMS.SysUserSinger.Update(singerPhoto);
                m_dbManager.LMS.SaveChanges();
                return true;
            }
            else if (SingerPhoto != null && SingerPhoto.Length > 0)
            {
                string FileName = AddPhoto(SingerPhoto);
                singerPhoto.SingerPhoto = "/images/" + FileName;
            }
            m_dbManager.LMS.SysUserSinger.Update(singerPhoto);
            m_dbManager.LMS.SaveChanges();
            return true;
        }

        public bool Add(IFormFile formFile)
         {
            var competition = _competition.GetStatus();
            if (competition.ToList().Count == 0)
            {
                return false;
            }
     
            int temp1 = 0;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
    
            // 使用EPPlus库打开Excel文件  
            using (var stream = new MemoryStream())
            {
                 formFile.CopyToAsync(stream);

                // 重置 MemoryStream 的位置以供读取  
                stream.Position = 0;
                using (var package = new ExcelPackage(stream))
                {
                    // 获取第一个工作表  
                    var worksheet = package.Workbook.Worksheets[0];
                    // 获取行数和列数  
                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                    // 遍历单元格并读取数据  
                    for (int row = 1; row <= rowCount; row++) // 假设第一行是标题行  
                    {

                        if (row == 1)
                        {
                            for (int col = 1; col <= colCount; col++)
                            {
                                temp1++;
                            }
                               
                        }
                        else
                        {
                            bool temp = true;
                            bool temp2 = true;
                            SysUserSinger sysUserSinger = new SysUserSinger();
                            sysUserSinger.SingerName = worksheet.Cells[row, temp1 - 5].Value?.ToString();
                            sysUserSinger.SingerPhoto = "/images/G.jpg";
                            sysUserSinger.SingerAge = worksheet.Cells[row, temp1 - 4].Value?.ToString();
                            if (worksheet.Cells[row, temp1 - 3].Value?.ToString() == "0")
                            {
                                temp = false;
                            }
                            sysUserSinger.Sex = temp;
                            sysUserSinger.SingerDescribe = worksheet.Cells[row, temp1 - 2].Value?.ToString();
                            sysUserSinger.Motto = worksheet.Cells[row, temp1 - 1].Value?.ToString();
                            if (worksheet.Cells[row, temp1].Value?.ToString()== "0")
                            {
                                temp2 = false;
                            }
                            sysUserSinger.Status = temp2;
                            foreach(var m in competition)
                            {
                                sysUserSinger.CompetitionsId= m.Id;
                            }
                            m_dbManager.LMS.SysUserSinger.Add(sysUserSinger);
                            m_dbManager.LMS.SaveChanges();
                        }
                    }
              
                }
            }
            return true;
         }//数据导入
    }
}
