namespace IdentityService.Application.Interfaces;
public interface IAppDbContext
{
    public DbSet<Cliente> Clientes { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
