using EgressProject.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EgressProject.API.Data.Mapping
{
    public class HighlightsEntityTypeConfiguration : IEntityTypeConfiguration<Highlights>
    {
        public void Configure(EntityTypeBuilder<Highlights> builder)
        {
            // Entity Configuration
            builder.ToTable("Highlights");
            builder.HasKey(hi => hi.Id);
            
            // Properties Configuration
            builder.Property(hi => hi.Id).HasColumnName("id").IsRequired();
            builder.Property(hi => hi.Title).HasColumnName("title").HasMaxLength(255).IsRequired();
            builder.Property(hi => hi.Description).HasColumnName("description").IsRequired();
            builder.Property(hi => hi.Link).HasColumnName("link").HasMaxLength(255);
            
            // Relationship Configuration
            builder.HasOne(hi => hi.Person)
                .WithMany(pe => pe.Highlights)
                .HasForeignKey(hi => hi.PersonId);
        }
    }
}