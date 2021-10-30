using EgressProject.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EgressProject.API.Data.Mapping
{
    public class JobAdvertisementEntityTypeConfiguration : IEntityTypeConfiguration<JobAdvertisement>
    {
        public void Configure(EntityTypeBuilder<JobAdvertisement> builder)
        {
            // Entity Configure
            builder.ToTable("JobAdvertisement");
            builder.HasKey(job => job.Id);

            // Properties Configure
            builder.Property(job => job.Id).HasColumnName("id").IsRequired();
            builder.Property(job => job.Title).HasColumnName("title").HasMaxLength(255).IsRequired();
            builder.Property(job => job.Company).HasColumnName("company").HasMaxLength(150).IsRequired();
            builder.Property(job => job.Description).HasColumnName("description").IsRequired();
            builder.Property(job => job.Modality).HasColumnName("modality").IsRequired();
            builder.Property(job => job.Benefit).HasColumnName("benefit");
            builder.Property(job => job.MinPayRange).HasColumnName("min_payrange").HasPrecision(8, 2);
            builder.Property(job => job.MaxPayRange).HasColumnName("max_payrange").HasPrecision(8, 2);
            builder.Property(job => job.Requerements).HasColumnName("requerements").IsRequired();
            builder.Property(job => job.MonthlyHours).HasColumnName("monthly_hours").IsRequired();
            builder.Property(job => job.Email).HasColumnName("email").HasMaxLength(100).IsRequired();
            builder.Property(job => job.PhoneNumber).HasColumnName("phone_number").HasMaxLength(20).IsRequired();
            builder.Property(job => job.Link).HasColumnName("link").HasMaxLength(500);
            builder.Property(job => job.DateLimit).HasColumnName("date_limit").IsRequired();
            builder.Property(job => job.City).HasColumnName("city").HasMaxLength(50).IsRequired();
            builder.Property(job => job.State).HasColumnName("state").HasMaxLength(50).IsRequired();
            builder.Property(job => job.Country).HasColumnName("country").HasMaxLength(50).IsRequired();

            // Relationship Configure
            builder.HasOne(job => job.User)
                .WithMany(user => user.JobAdvertisements)
                .HasForeignKey(job => job.UserId);
        }
    }
}