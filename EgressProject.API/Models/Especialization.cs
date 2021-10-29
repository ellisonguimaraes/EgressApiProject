using System;
using EgressProject.API.Models.Enums;

namespace EgressProject.API.Models
{
    public class Especialization : EntityBase
    {
        public string CourseName { get; set; }
        public string Intitution { get; set; }
        public CourseLevel Type { get; set; }
        public Status Status { get; set; }
        public Modality Modality { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        
        // Relationship
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
    }
}