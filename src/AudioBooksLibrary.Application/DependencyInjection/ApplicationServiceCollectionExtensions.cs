using AudioBooksLibrary.Application.Abstractions.Services;
using AudioBooksLibrary.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AudioBooksLibrary.Application.DependencyInjection;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Application.AssemblyReference.Assembly);
        
        services.AddScoped<IAudiobookService, AudiobookService>();
        services.AddScoped<IPlaybackService, PlaybackService>();
        services.AddScoped<IMarkerService, MarkerService>();
        return services;

        return services;
    }
}