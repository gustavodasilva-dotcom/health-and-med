using Common.Shared.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Common.Shared.Data;

public abstract class BaseDbContext<TDbContext>(
    IPublisher publisher,
    DbContextOptions<TDbContext> options
) : DbContext(options) where TDbContext : DbContext
{
    private readonly IPublisher _publisher = publisher;

    private async Task PublishDomainEventsAsync()
    {
        var domainEvents = ChangeTracker
            .Entries<DomainEvent>()
            .Select(entry => entry.Entity)
            .SelectMany(entity =>
            {
                var domainEvents = entity.GetDomainEvents();

                entity.ClearDomainEvents();

                return domainEvents;
            });

        foreach (var domainEvent in domainEvents)
        {
            await _publisher.Publish(domainEvent);
        }
    }

    public override async Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        var result = await base.SaveChangesAsync(cancellationToken);

        await PublishDomainEventsAsync();

        return result;
    }
}
