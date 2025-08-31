using System.ComponentModel.DataAnnotations;

namespace AudioBooksLibrary.Application.Abstractions.DTOs;

public record AudiobookDto(Guid Id, string Title, string? Description, string AuthorsLine, string? CoverUrl, int PartsCount);
public record AudiobookDetailsDto(Guid Id, string Title, string? Description, string AuthorsLine, string? CoverUrl, List<AudiobookPartDto> Parts);

public record AudiobookPartDto(Guid Id, int Index, string Title, string FilePath, string Md5, long FileSizeBytes, int DurationMs, string? MimeType);

public record PlaybackProgressDto(Guid AudiobookId, Guid? PartId, int PositionMs, bool IsCompleted, DateTime UpdatedAtUtc);

public record TimelineMarkerDto(Guid Id, Guid AudiobookId, Guid? PartId, int PositionMs, string? Text, Guid CreatedByUserId, string CreatedByUserName, int LikesCount, DateTime CreatedAtUtc);

public class CreateMarkerRequest
{
    [Required] public Guid AudiobookId { get; set; }
    public Guid? PartId { get; set; }
    [Range(0, int.MaxValue)] public int PositionMs { get; set; }
    [MaxLength(2000)] public string? Text { get; set; }
}

public class UpdateProgressRequest
{
    [Required] public Guid AudiobookId { get; set; }
    public Guid? PartId { get; set; }
    [Range(0, int.MaxValue)] public int PositionMs { get; set; }
    public bool IsCompleted { get; set; }
}

public record PagedResult<T>(IReadOnlyList<T> Items, int Total, int Skip, int Take);