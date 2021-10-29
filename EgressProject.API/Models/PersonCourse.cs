using System;
using EgressProject.API.Models.Enums;

namespace EgressProject.API.Models
{
    public class PersonCourse
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Mat { get; set; }
        public CourseLevel Level { get; set; }
        public Modality Modality { get; set; }
        public bool SelectCourse { get; set; }
        
        // Relationship
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }

        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
    }
}