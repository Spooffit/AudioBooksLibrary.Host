namespace AudioBooksLibrary.Core.Entities;

public class Audiobook
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string AuthorsLine { get; set; } = string.Empty;
    public string? CoverUrl { get; set; }
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAtUtc { get; set; } = DateTime.UtcNow;

    public ICollection<AudiobookPart> Parts { get; set; } = new List<AudiobookPart>();
    public ICollection<AudiobookAuthor> AudiobookAuthors { get; set; } = new List<AudiobookAuthor>();
}