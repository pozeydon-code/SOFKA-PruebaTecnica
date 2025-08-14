using IdentityService.Domain.Entities;
using IdentityService.Domain.Primitives;
using IdentityService.Application.Interfaces;

namespace IdentityService.Infrastructure.Persistence;
public class AppDbContext : DbContext, IAppDbContext, IUnitOfWork
{
    private readonly IPublisher _publisher;
    public AppDbContext(DbContextOptions options, IPublisher publisher) : base(options)
    {
        _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
    }
    public DbSet<Cliente> Clientes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var domainEvents = ChangeTracker.Entries<AggregateRoot>()
                                        .Select(e => e.Entity)
                                        .Where(e => e.GetDomainEvents().Any())
                                        .SelectMany(e => e.GetDomainEvents());

        var result = await base.SaveChangesAsync(cancellationToken);

        foreach (var domainEvent in domainEvents)
        {
            await _publisher.Publish(domainEvent, cancellationToken);
        }

        return result;
    }
}
