using System.Security.Cryptography;
using System.Text;
using AudioBooksLibrary.Core.Entities;
using AudioBooksLibrary.Core.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AudioBooksLibrary.Infrastructure.HostedServices;

public class LibraryIndexHostedServices(
    ILogger<LibraryIndexHostedServices> logger,
    IConfiguration config,
    IAudiobookRepository bookRepo,
    IAudiobookPartRepository partRepo,
    IUnitOfWork uow) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var root = config["Library:RootPath"];
        if (string.IsNullOrWhiteSpace(root) || !Directory.Exists(root))
        {
            logger.LogWarning("Library root path is not configured or does not exist.");
            return;
        }

        // Простейшая стратегия: каждая папка = книга, файлы внутри = части
        var dirs = Directory.GetDirectories(root);
        var idx = 0;
        foreach (var dir in dirs)
        {
            if (stoppingToken.IsCancellationRequested) break;
            var title = Path.GetFileName(dir);
            if (!await bookRepo.ExistsByTitleAsync(title, stoppingToken))
            {
                await bookRepo.AddAsync(new Audiobook
                {
                    Id = Guid.NewGuid(),
                    Title = title,
                    AuthorsLine = "", // можно парсить метаданные позже
                    Description = null
                }, stoppingToken);
            }
            await uow.SaveChangesAsync(stoppingToken);

            // Получим книгу
            var book = (await bookRepo.ListAsync(0, 1, stoppingToken))
                .FirstOrDefault(b => b.Title == title) 
                ?? throw new InvalidOperationException("Book should exist");

            var files = Directory.GetFiles(dir)
                .Where(f => IsAudio(f))
                .OrderBy(f => f, StringComparer.OrdinalIgnoreCase)
                .ToList();

            idx = 0;
            foreach (var file in files)
            {
                var md5 = await ComputeMd5Async(file, stoppingToken);
                var existing = await partRepo.GetByMd5Async(md5, stoppingToken);
                if (existing != null) continue;

                var fi = new FileInfo(file);
                var part = new AudiobookPart
                {
                    Id = Guid.NewGuid(),
                    AudiobookId = book.Id,
                    Index = idx++,
                    Title = Path.GetFileNameWithoutExtension(file),
                    FilePath = file,
                    Md5 = md5,
                    FileSizeBytes = fi.Length,
                    // DurationMs можно вычислить через ffprobe, но это вне EF/без внешних процессов тут оставим 0
                    DurationMs = 0,
                    MimeType = GuessMime(file)
                };
                await partRepo.AddAsync(part, stoppingToken);
            }
            await uow.SaveChangesAsync(stoppingToken);
        }
        logger.LogInformation("Library indexing finished.");
    }

    private static bool IsAudio(string path)
    {
        var ext = Path.GetExtension(path).ToLowerInvariant();
        return ext is ".mp3" or ".m4b" or ".m4a" or ".aac" or ".flac" or ".ogg" or ".wav";
    }

    private static string GuessMime(string path)
    {
        return Path.GetExtension(path).ToLowerInvariant() switch
        {
            ".mp3" => "audio/mpeg",
            ".m4a" or ".m4b" => "audio/mp4",
            ".aac" => "audio/aac",
            ".flac" => "audio/flac",
            ".ogg" => "audio/ogg",
            ".wav" => "audio/wav",
            _ => "application/octet-stream"
        };
    }

    private static async Task<string> ComputeMd5Async(string file, CancellationToken ct)
    {
        await using var stream = File.OpenRead(file);
        using var md5 = MD5.Create();
        var hash = await md5.ComputeHashAsync(stream, ct);
        var sb = new StringBuilder(hash.Length * 2);
        foreach (var b in hash) sb.Append(b.ToString("x2"));
        return sb.ToString();
    }
}