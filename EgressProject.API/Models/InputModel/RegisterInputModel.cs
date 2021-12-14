namespace EgressProject.API.Models.InputModel
{
    public class RegisterInputModel
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string DocNumber { get; set; }
        public string DocType { get; set; }
        public string Password { get; set; }
        public string PasswordRepeat { get; set; }
    }
}