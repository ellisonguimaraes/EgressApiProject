namespace EgressProject.API.Models.InputModel
{
    public class UserInputModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public bool? IsValidated { get; set; }

        public int? PersonId { get; set; }
    }
}