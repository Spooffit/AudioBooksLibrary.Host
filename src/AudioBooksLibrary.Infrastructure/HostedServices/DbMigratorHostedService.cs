using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AudioBooksLibrary.Infrastructure.HostedServices;

public sealed class DbMigratorHostedService<TContext>(IServiceProvider serviceProvider) : IHostedService
    where TContext : DbContext
{
    public async Task StartAsync(CancellationToken ct)
    {
        await using var scope = serviceProvider.CreateAsyncScope();
        var ctx = scope.ServiceProvider.GetRequiredService<TContext>();
        await ctx.Database.MigrateAsync(ct);
    }

    public Task StopAsync(CancellationToken _) => Task.CompletedTask;
}
