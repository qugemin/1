namespace CQIE.OnlineVote.DBManager
{
    public class DbManagerImp:IDbManager
    {

        private readonly CQIE.OnlineVote.DBManager.DbContexts.LMSDbContext m_Context;
        public DbManagerImp(CQIE.OnlineVote.DBManager.DbContexts.LMSDbContext context)
        {
            m_Context = context;
        }
        public CQIE.OnlineVote.DBManager.DbContexts.LMSDbContext LMS
        {
            get
            {
                return m_Context;
            }
        }
    }
}
