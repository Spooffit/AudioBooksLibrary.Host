using AudioBooksLibrary.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AudioBooksLibrary.Infrastructure;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Audiobook> Audiobooks => Set<Audiobook>();
    
    public DbSet<Author> Authors => Set<Author>();
    
    public DbSet<AudiobookAuthor> AudiobookAuthors => Set<AudiobookAuthor>();
    
    public DbSet<AudiobookPart> AudiobookParts => Set<AudiobookPart>();
    
    public DbSet<AppUser> Users => Set<AppUser>();
    
    public DbSet<PlaybackProgress> PlaybackProgresses => Set<PlaybackProgress>();
    
    public DbSet<TimelineMarker> TimelineMarkers => Set<TimelineMarker>();
    
    public DbSet<TimelineMarkerLike> TimelineMarkerLikes => Set<TimelineMarkerLike>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}