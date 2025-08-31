using AudioBooksLibrary.Application.Abstractions.DTOs;
using AudioBooksLibrary.Application.Abstractions.Services;
using AudioBooksLibrary.Core.Entities;
using AudioBooksLibrary.Core.Repositories;
using AutoMapper;

namespace AudioBooksLibrary.Application.Services;

public class MarkerService(ITimelineMarkerRepository repo, IUnitOfWork uow, IMapper mapper) : IMarkerService
{
    public async Task<PagedResult<TimelineMarkerDto>> GetByBookAsync(Guid bookId, Guid? partId, int skip, int take, CancellationToken ct)
    {
        var items = await repo.GetByBookAsync(bookId, partId, skip, take, ct);
        var mapped = items.Select(mapper.Map<TimelineMarkerDto>).ToList();
        return new PagedResult<TimelineMarkerDto>(mapped, mapped.Count, skip, take);
    }

    public async Task<TimelineMarkerDto> CreateAsync(Guid userId, CreateMarkerRequest req, CancellationToken ct)
    {
        var entity = new TimelineMarker
        {
            Id = Guid.NewGuid(),
            AudiobookId = req.AudiobookId,
            PartId = req.PartId,
            PositionMs = req.PositionMs,
            Text = req.Text,
            CreatedByUserId = userId,
            CreatedAtUtc = DateTime.UtcNow
        };
        await repo.AddAsync(entity, ct);
        await uow.SaveChangesAsync(ct);
        return mapper.Map<TimelineMarkerDto>(entity);
    }

    public async Task<bool> LikeAsync(Guid userId, Guid markerId, CancellationToken ct)
    {
        var added = await repo.AddLikeAsync(markerId, userId, ct);
        if (!added) return false;
        await uow.SaveChangesAsync(ct);
        return true;
    }
}