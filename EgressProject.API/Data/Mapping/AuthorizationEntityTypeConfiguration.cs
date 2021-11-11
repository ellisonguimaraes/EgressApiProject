using EgressProject.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EgressProject.API.Data.Mapping
{
    public class AuthorizationEntityTypeConfiguration : IEntityTypeConfiguration<Authorization>
    {
        public void Configure(EntityTypeBuilder<Authorization> builder)
        {
            // Entity Configure
            builder.ToTable("Authorization");
            builder.HasKey(au => au.Id);

            // Properties Configure 
            builder.Property(au => au.Id).HasColumnName("id").IsRequired();
            builder.Property(au => au.Token).HasColumnName("token").HasMaxLength(500).IsRequired();
            builder.Property(au => au.IpAddress).HasColumnName("ip_address").HasMaxLength(20).IsRequired();
            builder.Property(au => au.CreatedDate).HasColumnName("created_date").IsRequired();
            builder.Property(au => au.RefreshToken).HasColumnName("refresh_token").HasMaxLength(255).IsRequired();
            builder.Property(au => au.RefreshTokenExpiryTime).HasColumnName("refresh_token_expiry_time").IsRequired();
            builder.Property(au => au.IsValid).HasColumnName("is_valid").IsRequired();

            // Relationship Configure
            builder.HasOne(au => au.User)
                .WithMany(u => u.Authorizations)
                .HasForeignKey(au => au.UserId);
        }
    }
}