using AudioBooksLibrary.Core.Entities;

namespace AudioBooksLibrary.Core.Repositories;

public interface IPlaybackProgressRepository
{
    Task<PlaybackProgress?> GetAsync(Guid userId, Guid bookId, CancellationToken ct);
    Task UpsertAsync(PlaybackProgress entity, CancellationToken ct);
}