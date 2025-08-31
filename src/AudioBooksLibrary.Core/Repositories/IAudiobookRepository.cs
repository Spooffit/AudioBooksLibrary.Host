using AudioBooksLibrary.Core.Entities;

namespace AudioBooksLibrary.Core.Repositories;


public interface IAudiobookRepository
{
    Task<Audiobook?> GetAsync(Guid id, CancellationToken ct);
    Task<List<Audiobook>> ListAsync(int skip, int take, CancellationToken ct);
    Task AddAsync(Audiobook entity, CancellationToken ct);
    Task<bool> ExistsByTitleAsync(string title, CancellationToken ct);
}
