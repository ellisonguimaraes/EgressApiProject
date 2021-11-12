using System.Collections.Generic;
using EgressProject.API.Models.Enums;

namespace EgressProject.API.Models
{
    public class User : EntityBase
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public bool IsValidated { get; set; }

        // Relationship
        public int? PersonId { get; set; }
        public virtual Person Person { get; set; }

        public virtual IEnumerable<Authorization> Authorizations { get; set; }
        public virtual IEnumerable<News> News { get; set; }
        public virtual IEnumerable<JobAdvertisement> JobAdvertisements { get; set; }
    }
}