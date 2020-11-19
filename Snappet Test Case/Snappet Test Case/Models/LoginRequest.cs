namespace Snappet_Test_Case.Models
{
    public class LoginRequest
    {
        public string username { get; set; }
        public string password { get; set; }
        public string scope { get; set; }
        public string client_id { get; set; }
        public string client_secret { get; set; }
        public string grant_type { get; set; }
    }
}
