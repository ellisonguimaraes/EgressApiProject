using System;
using System.Collections.Generic;

namespace EgressProject.API.Models
{
    public class Person : EntityBase
    {
        public string Cpf { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public byte Sex { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneNumber2 { get; set; }
        public string PerfilImageSrc { get; set; }
        public bool ExposeData { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

        // Relationship
        public virtual User User { get; set; }
        public virtual IEnumerable<Highlights> Highlights { get; set; }
        public virtual IEnumerable<Especialization> Especializations { get; set; }
        public virtual IEnumerable<Testimony> Testimonies { get; set; }
        public virtual IEnumerable<Employment> Employments { get; set; }
        public virtual IEnumerable<PersonCourse> PersonCourses { get; set; }
    }
}