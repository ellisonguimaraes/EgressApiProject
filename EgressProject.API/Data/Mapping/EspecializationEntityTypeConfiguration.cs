using EgressProject.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EgressProject.API.Data.Mapping
{
    public class EspecializationEntityTypeConfiguration : IEntityTypeConfiguration<Especialization>
    {
        public void Configure(EntityTypeBuilder<Especialization> builder)
        {
            // Entity Configuration
            builder.ToTable("Especialization");
            builder.HasKey(es => es.Id);

            // Properties Configuration
            builder.Property(es => es.Id).HasColumnName("id").IsRequired();
            builder.Property(es => es.CourseName).HasColumnName("course_name").HasMaxLength(100).IsRequired();
            builder.Property(es => es.Intitution).HasColumnName("institution_name").HasMaxLength(100).IsRequired();
            builder.Property(es => es.Type).HasColumnName("type").IsRequired();
            builder.Property(es => es.Status).HasColumnName("status").IsRequired();
            builder.Property(es => es.Modality).HasColumnName("modality").IsRequired();
            builder.Property(es => es.StartDate).HasColumnName("start_date").IsRequired();
            builder.Property(es => es.EndDate).HasColumnName("end_date").IsRequired();

            // Relationship Configuration
            builder.HasOne(es => es.Person)
                .WithMany(pe => pe.Especializations)
                .HasForeignKey(es => es.PersonId);
        }
    }
}