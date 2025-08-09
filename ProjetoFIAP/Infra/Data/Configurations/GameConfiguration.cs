using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoFIAP.Api.Domain.Entities;

namespace ProjetoFIAP.Api.Infra.Data.Configurations
{
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.HasKey(g => g.Id);

            builder.Property(g => g.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(g => g.Description)
                .HasMaxLength(500);

            builder.Property(g => g.Price)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(g => g.Developer)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(g => g.Publisher)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(g => g.ReleaseDate)
                .IsRequired();
        }
    }
}