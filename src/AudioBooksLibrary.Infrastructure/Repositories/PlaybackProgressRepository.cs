using AudioBooksLibrary.Core.Entities;
using AudioBooksLibrary.Core.Repositories;
using AudioBooksLibrary.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AudioBooksLibrary.Infrastructure.Repositories;

public class PlaybackProgressRepository(AppDbContext db) : IPlaybackProgressRepository
{
    public Task<PlaybackProgress?> GetAsync(Guid userId, Guid bookId, CancellationToken ct) =>
        db.PlaybackProgresses.FirstOrDefaultAsync(x => x.UserId == userId && x.AudiobookId == bookId, ct);

    public async Task UpsertAsync(PlaybackProgress entity, CancellationToken ct)
    {
        var existing = await GetAsync(entity.UserId, entity.AudiobookId, ct);
        if (existing is null)
            await db.PlaybackProgresses.AddAsync(entity, ct);
        else
        {
            existing.PartId = entity.PartId;
            existing.PositionMs = entity.PositionMs;
            existing.IsCompleted = entity.IsCompleted;
            existing.UpdatedAtUtc = DateTime.UtcNow;
            existing.CompletedAtUtc = entity.IsCompleted ? DateTime.UtcNow : null;
        }
    }
}
