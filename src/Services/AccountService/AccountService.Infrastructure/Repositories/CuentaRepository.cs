using AccountService.Domain.Entities;
using AccountService.Application.Interfaces;
using AccountService.Infrastructure.Persistence;
using AccountService.Application.Dtos;

namespace AccountService.Infrastructure.Repositories;
public class CuentaRepository : ICuentaRepository
{
    private readonly AppDbContext _context;

    public CuentaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Cuenta>> GetAllAsync() => await _context.Set<Cuenta>().ToListAsync();
    public async Task<Cuenta?> GetByIdAsync(int id) => await _context.Set<Cuenta>().SingleOrDefaultAsync(e => e.NumeroCuenta == id);
    public async Task<bool> ExistsAsync(int id) => await _context.Set<Cuenta>().AnyAsync(e => e.NumeroCuenta == id);
    public async Task AddAsync(Cuenta entity) => await _context.Set<Cuenta>().AddAsync(entity);
    public void UpdateAsync(Cuenta entity) => _context.Set<Cuenta>().Update(entity);
    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
            _context.Set<Cuenta>().Remove(entity);
    }

    public async Task<List<CuentaSimple>> GetCuentasByNumeroCuentaAsync(int id) => await _context.Set<Cuenta>()
        .Where(c => c.NumeroCuenta == id && c.Estado)
        .Select(c => new CuentaSimple(c.NumeroCuenta, c.Tipo, c.SaldoInicial))
        .ToListAsync();

    public async Task<List<Cuenta>> GetByClienteIdAsync(int clienteId)
    {
        return await _context.Set<Cuenta>()
              .Where(c => c.ClienteId == clienteId && c.Estado)
              .ToListAsync();
    }
}
