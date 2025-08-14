namespace AccountService.Application.Interfaces;
public interface IAppDbContext
{

    public DbSet<Movimiento> Movimientos { get; set; }
    public DbSet<Cuenta> Cuentas { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
