using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace AudioBooksLibrary.Infrastructure;

public sealed class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var env = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Development";

        // Точка старта для конфигов: текущая папка вызова dotnet ef
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true)
            .AddJsonFile($"appsettings.{env}.json", optional: true)
            .AddEnvironmentVariables();

        var config = builder.Build();

        var conn = config.GetConnectionString("Postgres")
                   ?? throw new InvalidOperationException(
                       "ConnectionStrings:Postgres is missing for design-time.");

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(conn, b => b.MigrationsHistoryTable("__EFMigrationsHistory"))
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            .UseSnakeCaseNamingConvention()
            .Options;

        return new AppDbContext(options);
    }
}