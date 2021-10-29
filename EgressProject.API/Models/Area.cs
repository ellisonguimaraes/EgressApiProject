namespace EgressProject.API.Models
{
    public class Area
    {
        // Relationship
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
        
        public int JobId { get; set; }
        public virtual JobAdvertisement JobAdvertisement { get; set; }
    }
}