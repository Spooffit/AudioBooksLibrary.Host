using AudioBooksLibrary.Core.Entities;
using AudioBooksLibrary.Core.Repositories;
using AudioBooksLibrary.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AudioBooksLibrary.Infrastructure.Repositories;

public class TimelineMarkerRepository(AppDbContext db) : ITimelineMarkerRepository
{
    public Task<List<TimelineMarker>> GetByBookAsync(Guid bookId, Guid? partId, int skip, int take, CancellationToken ct)
    {
        var q = db.TimelineMarkers
            .Include(m => m.CreatedByUser)
            .Include(m => m.Likes)
            .Where(m => m.AudiobookId == bookId);

        if (partId.HasValue) q = q.Where(m => m.PartId == partId);

        return q.OrderBy(m => m.PositionMs).Skip(skip).Take(take).ToListAsync(ct);
    }

    public async Task AddAsync(TimelineMarker marker, CancellationToken ct) =>
        await db.TimelineMarkers.AddAsync(marker, ct);

    public async Task<bool> AddLikeAsync(Guid markerId, Guid userId, CancellationToken ct)
    {
        var exists = await db.TimelineMarkerLikes.AnyAsync(x => x.MarkerId == markerId && x.UserId == userId, ct);
        if (exists) return false;
        await db.TimelineMarkerLikes.AddAsync(new TimelineMarkerLike { MarkerId = markerId, UserId = userId }, ct);
        return true;
    }
}