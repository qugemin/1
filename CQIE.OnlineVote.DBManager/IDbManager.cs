using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQIE.OnlineVote.DBManager
{
    public interface IDbManager
    {
        public CQIE.OnlineVote.DBManager.DbContexts.LMSDbContext  LMS { get; }
    }
}
