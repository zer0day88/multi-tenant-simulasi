namespace multi_tenant.Helper
{
    public class DatabaseConnection
    {
        public string dbHost { get; set; }
        public string dbName { get; set; }
        public short dbPort { get; set; }

        public string getConnectionString()
        {
            return $"Server={dbHost};Port={dbPort};Database={dbName};User Id=admin;Password=Admin123_;";
        }
    }
}