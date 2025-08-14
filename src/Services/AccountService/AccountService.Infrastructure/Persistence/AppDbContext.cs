using AccountService.Domain.Entities;
using AccountService.Domain.Primitives;
using AccountService.Application.Interfaces;

namespace AccountService.Infrastructure.Persistence;
public class AppDbContext : DbContext, IAppDbContext, IUnitOfWork
{
    private readonly IPublisher _publisher;
    public AppDbContext(DbContextOptions options, IPublisher publisher) : base(options)
    {
        _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
    }
    public DbSet<Movimiento> Movimientos { get; set; }
    public DbSet<Cuenta> Cuentas { get; set; }

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
