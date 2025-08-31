using AudioBooksLibrary.Application.Abstractions.DTOs;

namespace AudioBooksLibrary.Application.Abstractions.Services;

public interface IPlaybackService
{
    Task<PlaybackProgressDto> UpsertAsync(Guid userId, UpdateProgressRequest request, CancellationToken ct);
    Task<PlaybackProgressDto?> GetAsync(Guid userId, Guid bookId, CancellationToken ct);
}