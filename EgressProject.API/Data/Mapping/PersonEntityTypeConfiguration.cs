using EgressProject.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EgressProject.API.Data.Mapping
{
    public class PersonEntityTypeConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            // Entity Configuration
            builder.ToTable("Person");
            builder.HasKey(pe => pe.Id);

            // Properties Configuration
            builder.Property(pe => pe.Cpf).HasColumnName("cpf").HasMaxLength(20).IsRequired();
            builder.Property(pe => pe.Name).HasColumnName("name").HasMaxLength(150).IsRequired();
            builder.Property(pe => pe.BirthDate).HasColumnName("birth_date").IsRequired();
            builder.Property(pe => pe.Sex).HasColumnName("sex").IsRequired();
            builder.Property(pe => pe.PhoneNumber).HasColumnName("phone_number").HasMaxLength(20).IsRequired();
            builder.Property(pe => pe.PhoneNumber2).HasColumnName("phone_number_2").HasMaxLength(20);
            builder.Property(pe => pe.PerfilImageSrc).HasColumnName("perfil_image").HasMaxLength(250);
            builder.Property(pe => pe.ExposeData).HasColumnName("expose_data").IsRequired();
            builder.Property(pe => pe.City).HasColumnName("city").HasMaxLength(50).IsRequired();
            builder.Property(pe => pe.State).HasColumnName("state").HasMaxLength(50).IsRequired();
            builder.Property(pe => pe.Country).HasColumnName("country").HasMaxLength(50).IsRequired();
        }
    }
}