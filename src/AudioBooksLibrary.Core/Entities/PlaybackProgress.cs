namespace AudioBooksLibrary.Core.Entities;

public class PlaybackProgress
{
    public Guid UserId { get; set; }
    public AppUser User { get; set; } = null!;

    public Guid AudiobookId { get; set; }
    public Audiobook Audiobook { get; set; } = null!;

    public Guid? PartId { get; set; }
    public AudiobookPart? Part { get; set; }

    public int PositionMs { get; set; } // смещение в текущей части
    public bool IsCompleted { get; set; }
    public DateTime UpdatedAtUtc { get; set; } = DateTime.UtcNow;
    public DateTime? CompletedAtUtc { get; set; }
}