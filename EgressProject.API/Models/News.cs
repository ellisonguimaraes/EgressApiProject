using System;

namespace EgressProject.API.Models
{
    public class News : EntityBase
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime PostDate { get; set; }
        public string ImgSrc { get; set; }
        public string Content { get; set; }

        // Relationship
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}