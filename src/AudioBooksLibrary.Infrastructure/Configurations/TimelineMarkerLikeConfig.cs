using AudioBooksLibrary.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AudioBooksLibrary.Infrastructure.Configurations;

public class TimelineMarkerLikeConfig : IEntityTypeConfiguration<TimelineMarkerLike>
{
    public void Configure(EntityTypeBuilder<TimelineMarkerLike> b)
    {
        b.HasKey(x => new { x.MarkerId, x.UserId });
        b.HasOne(x => x.Marker).WithMany(m => m.Likes).HasForeignKey(x => x.MarkerId);
        b.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);
    }
}