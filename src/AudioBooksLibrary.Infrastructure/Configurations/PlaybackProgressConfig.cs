using AudioBooksLibrary.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AudioBooksLibrary.Infrastructure.Configurations;

public class PlaybackProgressConfig : IEntityTypeConfiguration<PlaybackProgress>
{
    public void Configure(EntityTypeBuilder<PlaybackProgress> b)
    {
        b.HasKey(x => new { x.UserId, x.AudiobookId }); // по пользователю и книге
        b.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);
        b.HasOne(x => x.Audiobook).WithMany().HasForeignKey(x => x.AudiobookId);
        b.HasOne(x => x.Part).WithMany().HasForeignKey(x => x.PartId).OnDelete(DeleteBehavior.SetNull);
    }
}