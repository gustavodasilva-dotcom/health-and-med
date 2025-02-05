using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Shared.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations<TDbContext>(this IApplicationBuilder app)
        where TDbContext : DbContext
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using var context = scope
            .ServiceProvider.GetRequiredService<TDbContext>();

        //context.Database.Migrate();
    }
}
