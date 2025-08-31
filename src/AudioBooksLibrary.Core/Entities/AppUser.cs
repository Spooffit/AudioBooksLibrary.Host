namespace AudioBooksLibrary.Core.Entities;

public class AppUser
{
    public Guid Id { get; set; }
    public string DisplayName { get; set; } = null!;
}
