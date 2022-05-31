namespace multi_tenant.Models
{
    public record user_account
    {
        public long id_user { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public long id_tenant { get; set; }
    }
    
    public record mn_user_get_by_id_user_raw
    {
        public long id_user { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
}