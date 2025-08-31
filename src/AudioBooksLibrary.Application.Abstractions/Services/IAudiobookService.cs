using AudioBooksLibrary.Application.Abstractions.DTOs;

namespace AudioBooksLibrary.Application.Abstractions.Services;

public interface IAudiobookService
{
    Task<PagedResult<AudiobookDto>> ListAsync(int skip, int take, CancellationToken ct);
    Task<AudiobookDetailsDto?> GetAsync(Guid id, CancellationToken ct);
}