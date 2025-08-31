using AudioBooksLibrary.Application.Abstractions.DTOs;
using AudioBooksLibrary.Application.Abstractions.Services;
using AudioBooksLibrary.Core.Entities;
using AudioBooksLibrary.Core.Repositories;
using AutoMapper;

namespace AudioBooksLibrary.Application.Services;

public class PlaybackService(IPlaybackProgressRepository repo, IMapper mapper, IUnitOfWork uow) : IPlaybackService
{
    public async Task<PlaybackProgressDto> UpsertAsync(Guid userId, UpdateProgressRequest request, CancellationToken ct)
    {
        var entity = new PlaybackProgress
        {
            UserId = userId,
            AudiobookId = request.AudiobookId,
            PartId = request.PartId,
            PositionMs = request.PositionMs,
            IsCompleted = request.IsCompleted,
            UpdatedAtUtc = DateTime.UtcNow,
            CompletedAtUtc = request.IsCompleted ? DateTime.UtcNow : null
        };
        await repo.UpsertAsync(entity, ct);
        await uow.SaveChangesAsync(ct);
        return mapper.Map<PlaybackProgressDto>(entity);
    }

    public async Task<PlaybackProgressDto?> GetAsync(Guid userId, Guid bookId, CancellationToken ct)
    {
        var e = await repo.GetAsync(userId, bookId, ct);
        return e is null ? null : mapper.Map<PlaybackProgressDto>(e);
    }
}