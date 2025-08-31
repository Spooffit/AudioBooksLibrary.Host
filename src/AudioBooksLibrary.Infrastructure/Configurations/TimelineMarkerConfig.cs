using AudioBooksLibrary.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AudioBooksLibrary.Infrastructure.Configurations;

public class TimelineMarkerConfig : IEntityTypeConfiguration<TimelineMarker>
{
    public void Configure(EntityTypeBuilder<TimelineMarker> b)
    {
        b.HasKey(x => x.Id);
        b.Property(x => x.Text).HasMaxLength(2000);
        b.HasOne(x => x.Audiobook).WithMany().HasForeignKey(x => x.AudiobookId);
        b.HasOne(x => x.Part).WithMany().HasForeignKey(x => x.PartId).OnDelete(DeleteBehavior.Cascade);
        b.HasOne(x => x.CreatedByUser).WithMany().HasForeignKey(x => x.CreatedByUserId);
        b.HasIndex(x => new { x.AudiobookId, x.PartId, x.PositionMs });
    }
}