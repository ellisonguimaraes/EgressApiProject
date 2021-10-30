using EgressProject.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EgressProject.API.Data.Mapping
{
    public class CourseEntityTypeConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            // Entity Configuration
            builder.ToTable("Course");
            builder.HasKey(co => co.Id);

            // Properties Configuration
            builder.Property(co => co.Id).HasColumnName("id").IsRequired();
            builder.Property(co => co.CourseName).HasColumnName("course_name").HasMaxLength(150).IsRequired();
        }
    }
}