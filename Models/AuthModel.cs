namespace multi_tenant.Models
{
    
    public class AuthenticationRequest
    {
        public string email { get; set; }
        public string password { get; set; }
        public long id_tenant { get; set; }
    }
    
    public class AuthenticationResponse
    {
        public long id_user { get; set; }
        public string email { get; set; }
        public long id_tenant { get; set; }
        
        public string Token { get; set; }
    }
}