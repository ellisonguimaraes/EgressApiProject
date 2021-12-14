using EgressProject.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EgressProject.API.Data.Mapping
{
    public class EmploymentEntityTypeConfiguration : IEntityTypeConfiguration<Employment>
    {
        public void Configure(EntityTypeBuilder<Employment> builder)
        {
            // Entity Configuration
            builder.ToTable("Employment");
            builder.HasKey(em => em.Id);

            // Properties Configuration
            builder.Property(em => em.Id).HasColumnName("id").IsRequired();
            builder.Property(em => em.Role).HasColumnName("role").HasMaxLength(100).IsRequired();
            builder.Property(em => em.Enterprise).HasColumnName("enterprise").HasMaxLength(100).IsRequired();
            builder.Property(em => em.Section).HasColumnName("section").HasMaxLength(100).IsRequired();
            builder.Property(em => em.Salary).HasColumnName("salary").HasPrecision(8, 2).IsRequired();
            builder.Property(em => em.Initiative).HasColumnName("initiative").IsRequired();
            builder.Property(em => em.Status).HasColumnName("status").IsRequired();
            builder.Property(em => em.StartDate).HasColumnName("start_date").IsRequired();
            builder.Property(em => em.EndDate).HasColumnName("end_date").IsRequired();
            builder.Property(em => em.PersonId).HasColumnName("person_id").IsRequired();

            // Relationship Configuration
            builder.HasOne(em => em.Person)
                .WithMany(pe => pe.Employments)
                .HasForeignKey(em => em.PersonId);
        }
    }
}