namespace EgressProject.API.Models
{
    public class Highlights : EntityBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
    
        // Relationship
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
    }
}