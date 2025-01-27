namespace WebProjrctManagement.Data
{
    public class StatisticsRepo
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public StatisticsRepo(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("ConnectionString");
        }
    }
}
