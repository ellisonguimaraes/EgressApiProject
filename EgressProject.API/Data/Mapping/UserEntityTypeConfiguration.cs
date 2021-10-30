using EgressProject.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EgressProject.API.Data.Mapping
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Entity Configure
            builder.ToTable("User");
            builder.HasKey(user => user.Id);

            // Properties Configure
            builder.Property(user => user.Id).HasColumnName("id").IsRequired();
            builder.Property(user => user.Email).HasColumnName("email").HasMaxLength(100).IsRequired();
            builder.Property(user => user.Password).HasColumnName("password").HasMaxLength(25).IsRequired();
            builder.Property(user => user.Role).HasColumnName("role").IsRequired();
            builder.Property(user => user.IsValidated).HasColumnName("is_validated").IsRequired();

            // Relationship Configure
            builder.HasOne(user => user.Person)
                .WithOne(pe => pe.User)
                .HasForeignKey<User>(user => user.PersonId);
        }
    }
}