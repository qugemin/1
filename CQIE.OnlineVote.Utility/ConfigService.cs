namespace CQIE.OnlineVote.Utility
{

    public class ConfigService
    {
        private Microsoft.Extensions.Configuration.IConfiguration _configuration;
        public ConfigService(Microsoft.Extensions.Configuration.IConfiguration configuration) { 
            _configuration = configuration;
        }
        public string  GetConnectionString()
        {
            return _configuration["ConnectionStrings:LMSDB"];
        }


    }
}
