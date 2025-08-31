namespace AudioBooksLibrary.Core.Entities;

public class AudiobookPart
{
    public Guid Id { get; set; }
    public Guid AudiobookId { get; set; }
    public Audiobook Audiobook { get; set; } = null!;

    public int Index { get; set; } // порядок частей
    public string Title { get; set; } = string.Empty;

    // Файл
    public string FilePath { get; set; } = null!;
    public string Md5 { get; set; } = null!; // ключ индексации
    public long FileSizeBytes { get; set; }
    public int DurationMs { get; set; } // длительность
    public string? MimeType { get; set; }

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}