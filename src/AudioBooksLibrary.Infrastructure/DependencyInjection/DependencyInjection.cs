using AudioBooksLibrary.Core.Repositories;
using AudioBooksLibrary.Infrastructure.HostedServices;
using AudioBooksLibrary.Infrastructure.Persistence;
using AudioBooksLibrary.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AudioBooksLibrary.Infrastructure.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        var conn = config.GetConnectionString("Postgres")
                   ?? throw new InvalidOperationException("ConnectionStrings:Postgres is missing");

        services.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseNpgsql(conn, npg => npg.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName));
        });

        // Репозитории
        services.AddScoped<IAudiobookRepository, AudiobookRepository>();
        services.AddScoped<IAudiobookPartRepository, AudiobookPartRepository>();
        services.AddScoped<IPlaybackProgressRepository, PlaybackProgressRepository>();
        services.AddScoped<ITimelineMarkerRepository, TimelineMarkerRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Индексация файлов (хост-сервис)
        services.AddHostedService<LibraryIndexHostedServices>();

        return services;
    }
}