using AudioBooksLibrary.Application.Abstractions.DTOs;

namespace AudioBooksLibrary.Application.Abstractions.Services;

public interface IMarkerService
{
    Task<PagedResult<TimelineMarkerDto>> GetByBookAsync(Guid bookId, Guid? partId, int skip, int take, CancellationToken ct);
    Task<TimelineMarkerDto> CreateAsync(Guid userId, CreateMarkerRequest req, CancellationToken ct);
    Task<bool> LikeAsync(Guid userId, Guid markerId, CancellationToken ct);
}
