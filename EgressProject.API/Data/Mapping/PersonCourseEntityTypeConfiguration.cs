using EgressProject.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EgressProject.API.Data.Mapping
{
    public class PersonCourseEntityTypeConfiguration : IEntityTypeConfiguration<PersonCourse>
    {
        public void Configure(EntityTypeBuilder<PersonCourse> builder)
        {
            // Entity Configuration
            builder.ToTable("Person_Course");
            builder.HasKey(pc => new { pc.PersonId, pc.CourseId });

            // Properties Configuration
            builder.Property(pc => pc.PersonId).HasColumnName("person_id").IsRequired();
            builder.Property(pc => pc.CourseId).HasColumnName("course_id").IsRequired();
            builder.Property(pc => pc.StartDate).HasColumnName("start_date").IsRequired();
            builder.Property(pc => pc.EndDate).HasColumnName("end_date").IsRequired();
            builder.Property(pc => pc.Mat).HasColumnName("mat").HasMaxLength(20).IsRequired();
            builder.Property(pc => pc.Level).HasColumnName("level").IsRequired();
            builder.Property(pc => pc.Modality).HasColumnName("modality").IsRequired();
            builder.Property(pc => pc.SelectCourse).HasColumnName("select_course").IsRequired();

            // Relationship Configuration
            builder.HasOne(pc => pc.Person)
                .WithMany(pe => pe.PersonCourses)
                .HasForeignKey(pc => pc.PersonId);

            builder.HasOne(pc => pc.Course)
                .WithMany(co => co.PersonCourses)
                .HasForeignKey(pc => pc.CourseId);
        }
    }
}