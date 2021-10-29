using System;
using EgressProject.API.Models.Enums;

namespace EgressProject.API.Models
{
    public class Employment : EntityBase
    {
        public string Role { get; set; }
        public string Enterprise { get; set; }
        public string Section { get; set; }
        public decimal Salary { get; set; }
        public Initiative Initiative { get; set; }
        public Status Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        
        // Relationship
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
    }
}