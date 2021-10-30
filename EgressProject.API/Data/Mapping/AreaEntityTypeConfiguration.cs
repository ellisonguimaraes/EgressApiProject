using EgressProject.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EgressProject.API.Data.Mapping
{
    public class AreaEntityTypeConfiguration : IEntityTypeConfiguration<Area>
    {
        public void Configure(EntityTypeBuilder<Area> builder)
        {
            // Entity Configuration
            builder.ToTable("Area");
            builder.HasKey(a => new {a.CourseId, a.JobId});

            // Property Configuration
            builder.Property(a => a.CourseId).HasColumnName("course_id").IsRequired();
            builder.Property(a => a.JobId).HasColumnName("job_id").IsRequired();

            // Relationship Configuration
            builder.HasOne(a => a.Course)
                .WithMany(c => c.Areas)
                .HasForeignKey(a => a.CourseId);

            builder.HasOne(a => a.JobAdvertisement)
                .WithMany(ja => ja.Areas)
                .HasForeignKey(a => a.JobId);
        }
    }
}