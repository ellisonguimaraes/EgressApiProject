using EgressProject.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EgressProject.API.Data.Mapping
{
    public class TestimonyEntityTypeConfiguration : IEntityTypeConfiguration<Testimony>
    {
        public void Configure(EntityTypeBuilder<Testimony> builder)
        {
            // Entity Configure
            builder.ToTable("Testimony");
            builder.HasKey(te => te.Id);

            // Properties Configure
            builder.Property(te => te.Id).HasColumnName("id").IsRequired();
            builder.Property(te => te.Content).HasColumnName("content").IsRequired();
            builder.Property(te => te.PostDate).HasColumnName("post_date").IsRequired();

            // Relationship Configure
            builder.HasOne(te => te.Person)
                .WithMany(pe => pe.Testimonies)
                .HasForeignKey(te => te.PersonId);
        }
    }
}