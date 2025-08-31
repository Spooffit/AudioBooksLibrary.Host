using AudioBooksLibrary.Core.Entities;
using AudioBooksLibrary.Core.Repositories;
using AudioBooksLibrary.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AudioBooksLibrary.Infrastructure.Repositories;

public class AudiobookPartRepository(AppDbContext db) : IAudiobookPartRepository
{
    public Task<AudiobookPart?> GetByMd5Async(string md5, CancellationToken ct) =>
        db.AudiobookParts.FirstOrDefaultAsync(p => p.Md5 == md5, ct);

    public async Task AddAsync(AudiobookPart part, CancellationToken ct) =>
        await db.AudiobookParts.AddAsync(part, ct);

    public Task<List<AudiobookPart>> GetByBookAsync(Guid bookId, CancellationToken ct) =>
        db.AudiobookParts.Where(p => p.AudiobookId == bookId).OrderBy(p => p.Index).ToListAsync(ct);
}