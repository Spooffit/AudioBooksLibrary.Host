using AudioBooksLibrary.Core.Entities;

namespace AudioBooksLibrary.Core.Repositories;

public interface ITimelineMarkerRepository
{
    Task<List<TimelineMarker>> GetByBookAsync(Guid bookId, Guid? partId, int skip, int take, CancellationToken ct);
    Task AddAsync(TimelineMarker marker, CancellationToken ct);
    Task<bool> AddLikeAsync(Guid markerId, Guid userId, CancellationToken ct);
}