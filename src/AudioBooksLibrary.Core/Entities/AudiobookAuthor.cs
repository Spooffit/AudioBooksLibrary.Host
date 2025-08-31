namespace AudioBooksLibrary.Core.Entities;

public class AudiobookAuthor
{
    public Guid AudiobookId { get; set; }
    public Audiobook Audiobook { get; set; } = null!;
    public Guid AuthorId { get; set; }
    public Author Author { get; set; } = null!;
}
