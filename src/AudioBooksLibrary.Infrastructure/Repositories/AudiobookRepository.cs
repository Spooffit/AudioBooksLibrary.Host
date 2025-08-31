using AudioBooksLibrary.Core.Entities;
using AudioBooksLibrary.Core.Repositories;
using AudioBooksLibrary.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AudioBooksLibrary.Infrastructure.Repositories;

public class AudiobookRepository(AppDbContext db) : IAudiobookRepository
{
    public Task<Audiobook?> GetAsync(Guid id, CancellationToken ct) =>
        db.Audiobooks
            .Include(a => a.Parts.OrderBy(p => p.Index))
            .FirstOrDefaultAsync(a => a.Id == id, ct);

    public Task<List<Audiobook>> ListAsync(int skip, int take, CancellationToken ct) =>
        db.Audiobooks
            .OrderByDescending(a => a.CreatedAtUtc)
            .Skip(skip).Take(take).ToListAsync(ct);

    public async Task AddAsync(Audiobook entity, CancellationToken ct)
    {
        await db.Audiobooks.AddAsync(entity, ct);
    }

    public Task<bool> ExistsByTitleAsync(string title, CancellationToken ct) =>
        db.Audiobooks.AnyAsync(a => a.Title == title, ct);
}
