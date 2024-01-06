using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CQIE.OnlineVote.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
namespace CQIE.OnlineVote.DBManager.DbContexts
{
    public class LMSDbContext:DbContext/*:IdentityDbContext//框架加密表*/
    { private string m_ConnectionString;
        public LMSDbContext(CQIE.OnlineVote.Utility.ConfigService configService)
        {
            m_ConnectionString = configService.GetConnectionString();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
 
            optionsBuilder.UseSqlServer(m_ConnectionString);
            base.OnConfiguring(optionsBuilder);
        }
        #region 建立每个表的使用
       public DbSet<CQIE.OnlineVote.Models.Sysuser> Sysuser { get; set; }
        public DbSet<CQIE.OnlineVote.Models.SysRole > SysRole { get; set; }
        public DbSet<CQIE.OnlineVote.Models.USerRole> USerRole { get; set; }

        public DbSet<CQIE.OnlineVote.Models.SysuerInformation> SysuerInformation { get; set; }
        public DbSet<CQIE.OnlineVote.Models.SysUserSinger> SysUserSinger { get; set; }
        public DbSet<CQIE.OnlineVote.Models.SystemMenu> SystemMenu { get; set; }
        public DbSet<CQIE.OnlineVote.Models.MenuRole> MenuRole { get; set; }
        public DbSet<CQIE.OnlineVote.Models.Competition> Competition { get; set; }
        public DbSet<CQIE.OnlineVote.Models.CompetitionRound> CompetitionRound { get; set; }
        public DbSet<CQIE.OnlineVote.Models.Battle> Battle { get; set; } 
        public DbSet<CQIE.OnlineVote.Models.Vote> Vote { get; set; }
        public DbSet<CQIE.OnlineVote.Models.BattleUser> Temp { get; set; }
        #endregion

        #region //表的关系
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CQIE.OnlineVote.Models.Sysuser>(entity =>
            {
                entity.HasKey(o => o.Id);
            });
            modelBuilder.Entity<CQIE.OnlineVote.Models.SysRole>(entity =>
            {
                entity.HasKey(o => o.Id);

            });
            modelBuilder.Entity<CQIE.OnlineVote.Models.USerRole>(entity =>
            {
                entity.HasKey(o => o.Id);
                entity.HasOne(p => p.Sysusers).WithMany(q => q.USerRoles).HasForeignKey(p => p.UId);
                entity.HasOne(p => p.Roles).WithMany(q => q.USerRoles).HasForeignKey(p => p.RId);
            });
            modelBuilder.Entity<CQIE.OnlineVote.Models.SysuerInformation>(entity =>
            {
                entity.HasKey(o => o.Id);
                entity.HasOne(p => p.Sysusers).WithOne(q => q.SyserInfos).HasForeignKey<SysuerInformation>(p => p.UID);
            });
            modelBuilder.Entity<CQIE.OnlineVote.Models.SystemMenu>(entity =>
            {
                entity.HasKey(o => o.Id);
            });
            modelBuilder.Entity<CQIE.OnlineVote.Models.MenuRole>(entity =>
            {
                entity.HasKey(o => o.Id);
                entity.HasOne(p => p.Roels).WithMany(q => q.MenuRoles).HasForeignKey(p => p.RoleId);
                entity.HasOne(p => p.Menus).WithMany(q => q.MenuRoles).HasForeignKey(p => p.MenuId);
            });
            modelBuilder.Entity<CQIE.OnlineVote.Models.Competition>(entity =>
            {
                entity.HasKey(o => o.Id);
            });
            modelBuilder.Entity<CQIE.OnlineVote.Models.SysUserSinger>(entity =>
            {
                entity.HasKey(o => o.Id);
                entity.HasOne(p => p.Competitions).WithMany(q => q.SysUserSingers).HasForeignKey(p => p.CompetitionsId);
            });
            modelBuilder.Entity<CQIE.OnlineVote.Models.CompetitionRound>(entity =>
            {
                entity.HasKey(o => o.Id);
                entity.HasOne(p => p.Competitions).WithMany(q => q.CompetitionRounds).HasForeignKey(p => p.CompetitionsId);
            });
            modelBuilder.Entity<CQIE.OnlineVote.Models.Battle>(entity =>
            {
                entity.HasKey(o => o.Id);
            });
            modelBuilder.Entity<CQIE.OnlineVote.Models.Vote>(entity =>
            {
                entity.HasKey(o => o.Id);
                entity.HasOne(p => p.CompetitionRounds).WithMany(q => q.Votes).HasForeignKey(q => q.RoundId);
            });
            modelBuilder.Entity<CQIE.OnlineVote.Models.BattleUser>(entity =>
            {
                entity.HasKey(o => o.Id);
                entity.HasOne(p => p.Battle).WithMany(q => q.Temp).HasForeignKey(q => q.BattleId);
                entity.HasOne(p => p.User).WithMany(q => q.Temp).HasForeignKey(q => q.UserId);
            });
            base.OnModelCreating(modelBuilder);

        }
        #endregion
    }
}
