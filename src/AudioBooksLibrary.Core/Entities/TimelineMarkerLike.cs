namespace AudioBooksLibrary.Core.Entities;

public class TimelineMarkerLike
{
    public Guid MarkerId { get; set; }
    public TimelineMarker Marker { get; set; } = null!;

    public Guid UserId { get; set; }
    public AppUser User { get; set; } = null!;

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}