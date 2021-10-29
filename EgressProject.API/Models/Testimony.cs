using System;

namespace EgressProject.API.Models
{
    public class Testimony : EntityBase
    {
        public string Content { get; set; }
        public DateTime PostDate { get; set; }
        
        // Relationship
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
    }
}