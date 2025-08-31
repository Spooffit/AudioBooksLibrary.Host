using AudioBooksLibrary.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AudioBooksLibrary.Infrastructure.Configurations;

public class AudiobookPartConfig : IEntityTypeConfiguration<AudiobookPart>
{
    public void Configure(EntityTypeBuilder<AudiobookPart> b)
    {
        b.HasKey(x => x.Id);
        b.Property(x => x.FilePath).IsRequired().HasMaxLength(2048);
        b.Property(x => x.Md5).IsRequired().HasMaxLength(32);
        b.HasIndex(x => x.Md5).IsUnique();
        b.HasIndex(x => new { x.AudiobookId, x.Index }).IsUnique();
        b.Property(x => x.Title).HasMaxLength(512);
    }
}