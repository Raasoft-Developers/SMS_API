namespace Nvg.SMSService.Data.Models
{
    public class SMSDBInfo
    {
        public SMSDBInfo(string connectionString, string databaseProvider)
        {
            ConnectionString = connectionString;
            DatabaseProvider = databaseProvider;
        }

        public string ConnectionString { get; set; }
        public string DatabaseProvider { get; set; }
    }
}
