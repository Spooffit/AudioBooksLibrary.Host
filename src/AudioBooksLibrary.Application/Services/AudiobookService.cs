using AudioBooksLibrary.Application.Abstractions.DTOs;
using AudioBooksLibrary.Application.Abstractions.Services;
using AudioBooksLibrary.Core.Repositories;
using AutoMapper;

namespace AudioBooksLibrary.Application.Services;


public class AudiobookService(IAudiobookRepository repo, IMapper mapper) : IAudiobookService
{
    public async Task<PagedResult<AudiobookDto>> ListAsync(int skip, int take, CancellationToken ct)
    {
        var items = await repo.ListAsync(skip, take, ct);
        var mapped = items.Select(mapper.Map<AudiobookDto>).ToList();
        return new PagedResult<AudiobookDto>(mapped, mapped.Count, skip, take);
    }

    public async Task<AudiobookDetailsDto?> GetAsync(Guid id, CancellationToken ct)
    {
        var entity = await repo.GetAsync(id, ct);
        return entity is null ? null : mapper.Map<AudiobookDetailsDto>(entity);
    }
}