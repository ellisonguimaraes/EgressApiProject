namespace EgressProject.API.Models
{
    public class Authorization : EntityBase
    {
        public string Token { get; set; }
        public string IpAddress { get; set; }
        public string RefreshToken { get; set; }
        public string RefreshTokenExpiryTime { get; set; }
        public bool IsValid { get; set; }   
        
        // Relationship 
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}