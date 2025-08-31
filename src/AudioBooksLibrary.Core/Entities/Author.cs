namespace AudioBooksLibrary.Core.Entities;

public class Author
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<AudiobookAuthor> AudiobookAuthors { get; set; } = new List<AudiobookAuthor>();
}