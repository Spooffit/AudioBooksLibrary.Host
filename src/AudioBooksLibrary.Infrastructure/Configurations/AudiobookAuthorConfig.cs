using AudioBooksLibrary.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AudioBooksLibrary.Infrastructure.Configurations;

public class AudiobookAuthorConfig : IEntityTypeConfiguration<AudiobookAuthor>
{
    public void Configure(EntityTypeBuilder<AudiobookAuthor> b)
    {
        b.HasKey(x => new { x.AudiobookId, x.AuthorId });
        b.HasOne(x => x.Audiobook).WithMany(a => a.AudiobookAuthors).HasForeignKey(x => x.AudiobookId);
        b.HasOne(x => x.Author).WithMany(a => a.AudiobookAuthors).HasForeignKey(x => x.AuthorId);
    }
}