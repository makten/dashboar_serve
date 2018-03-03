namespace dashboard.Core.Models
{
    public class AuthenticationToken
    {
        public string Access_Token { get; set; }
        public string Token_Type { get; set; }
        public double Expires_In { get; set; }
        public string Refresh_Token { get; set; }
    }
}