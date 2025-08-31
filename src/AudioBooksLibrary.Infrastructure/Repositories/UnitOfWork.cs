using AudioBooksLibrary.Core.Repositories;
using AudioBooksLibrary.Infrastructure.Persistence;

namespace AudioBooksLibrary.Infrastructure.Repositories;

public class UnitOfWork(AppDbContext db) : IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken ct = default) => db.SaveChangesAsync(ct);
}