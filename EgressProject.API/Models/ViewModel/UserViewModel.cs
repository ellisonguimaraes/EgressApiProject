using System.Collections.Generic;

namespace EgressProject.API.Models.ViewModel
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public bool IsValidated { get; set; }

        // Relationship
        public int? PersonId { get; set; }
        public virtual Person Person { get; set; }

        public virtual IEnumerable<News> News { get; set; }
        public virtual IEnumerable<JobAdvertisement> JobAdvertisements { get; set; }
    }
}