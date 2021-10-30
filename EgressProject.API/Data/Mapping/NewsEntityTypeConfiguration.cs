using EgressProject.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EgressProject.API.Data.Mapping
{
    public class NewsEntityTypeConfiguration : IEntityTypeConfiguration<News>
    {
        public void Configure(EntityTypeBuilder<News> builder)
        {
            // Entity Configure
            builder.ToTable("News");
            builder.HasKey(ne => ne.Id);

            // Properties Configure
            builder.Property(ne => ne.Id).HasColumnName("id").IsRequired();
            builder.Property(ne => ne.Title).HasColumnName("title").HasMaxLength(255).IsRequired();
            builder.Property(ne => ne.Author).HasColumnName("author").HasMaxLength(255).IsRequired();
            builder.Property(ne => ne.PostDate).HasColumnName("post_date").IsRequired();
            builder.Property(ne => ne.ImgSrc).HasColumnName("img_src").HasMaxLength(255);
            builder.Property(ne => ne.Content).HasColumnName("content").IsRequired();

            // Relationship Configure
            builder.HasOne(ne => ne.User)
                .WithMany(user => user.News)
                .HasForeignKey(ne => ne.UserId);
        }
    }
}