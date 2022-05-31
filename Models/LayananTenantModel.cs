namespace multi_tenant.Models
{
    public record layanan_tenant_get_config
    {
        public long id_user { get; set; }
        public long id_tenant { get; set; }
        public string db_host { get; set; }
        public string db_name { get; set; }
        public string db_port { get; set; }
        
        public string getConnectionString()
        {
            return $"Server={db_host};Port={db_port};Database={db_name};User Id=admin;Password=Admin123_;";
        }
    }
}