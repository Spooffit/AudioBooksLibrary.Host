namespace AudioBooksLibrary.Core.Entities;


public class TimelineMarker
{
    public Guid Id { get; set; }

    public Guid AudiobookId { get; set; }
    public Audiobook Audiobook { get; set; } = null!;

    public Guid? PartId { get; set; }
    public AudiobookPart? Part { get; set; }

    public int PositionMs { get; set; } // точка на таймлинии
    public string? Text { get; set; }   // комментарий (опционально)

    public Guid CreatedByUserId { get; set; }
    public AppUser CreatedByUser { get; set; } = null!;
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

    public ICollection<TimelineMarkerLike> Likes { get; set; } = new List<TimelineMarkerLike>();
}