using AudioBooksLibrary.Core.Entities;

namespace AudioBooksLibrary.Core.Repositories;

public interface IAudiobookPartRepository
{
    Task<AudiobookPart?> GetByMd5Async(string md5, CancellationToken ct);
    Task AddAsync(AudiobookPart part, CancellationToken ct);
    Task<List<AudiobookPart>> GetByBookAsync(Guid bookId, CancellationToken ct);
}