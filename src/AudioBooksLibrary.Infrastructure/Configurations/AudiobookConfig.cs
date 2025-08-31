using AudioBooksLibrary.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AudioBooksLibrary.Infrastructure.Configurations;

public class AudiobookConfig : IEntityTypeConfiguration<Audiobook>
{
    public void Configure(EntityTypeBuilder<Audiobook> b)
    {
        b.HasKey(x => x.Id);
        b.Property(x => x.Title).IsRequired().HasMaxLength(512);
        b.Property(x => x.AuthorsLine).HasMaxLength(512);
        b.Property(x => x.CoverUrl).HasMaxLength(1024);
        b.HasMany(x => x.Parts)
            .WithOne(p => p.Audiobook)
            .HasForeignKey(p => p.AudiobookId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}