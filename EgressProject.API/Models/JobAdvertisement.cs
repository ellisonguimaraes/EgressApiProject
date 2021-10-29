using System;
using System.Collections.Generic;
using EgressProject.API.Models.Enums;

namespace EgressProject.API.Models
{
    public class JobAdvertisement : EntityBase
    {
        public string Title { get; set; }
        public string Company { get; set; }
        public string Description { get; set; }
        public Modality Modality { get; set; }
        public string Benefit { get; set; }
        public decimal MinPayRange { get; set; }
        public decimal MaxPayRange { get; set; }
        public string Requerements { get; set; }
        public int MonthlyHours { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Link { get; set; }
        public DateTime DateLimit { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

        // Relationship
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public virtual IEnumerable<Area> Areas { get; set; }
    }
}