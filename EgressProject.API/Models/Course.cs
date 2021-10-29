using System.Collections.Generic;

namespace EgressProject.API.Models
{
    public class Course : EntityBase
    {
        public string CourseName { get; set; }

        // Relationship
        public virtual IEnumerable<PersonCourse> PersonCourses { get; set; }
        public virtual IEnumerable<Area> Areas { get; set; }
    }
}