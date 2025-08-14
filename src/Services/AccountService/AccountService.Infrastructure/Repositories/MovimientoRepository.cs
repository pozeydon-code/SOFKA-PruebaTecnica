using AccountService.Domain.Entities;
using AccountService.Application.Interfaces;
using AccountService.Infrastructure.Persistence;
using AccountService.Application.Dtos;
using AccountService.Domain.ValueObjects;

namespace AccountService.Infrastructure.Repositories;
public class MovimientoRepository : IMovimientoRepository
{
    private readonly AppDbContext _context;

    public MovimientoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Movimiento>> GetAllAsync() => await _context.Set<Movimiento>().ToListAsync();
    public async Task<Movimiento?> GetByIdAsync(int id) => await _context.Set<Movimiento>().SingleOrDefaultAsync(e => e.Id == id);
    public async Task<bool> ExistsAsync(int id) => await _context.Set<Movimiento>().AnyAsync(e => e.Id == id);
    public async Task AddAsync(Movimiento entity) => await _context.Set<Movimiento>().AddAsync(entity);
    public void UpdateAsync(Movimiento entity) => _context.Set<Movimiento>().Update(entity);
    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
            _context.Set<Movimiento>().Remove(entity);
    }

    public async Task<List<EstadoCuenta_MovimientoDto>> GetMovimientosByRangeAsync(DateTime fechaInicio, DateTime fechaFin, int numeroCuenta)
    {
        return await _context.Set<Movimiento>().Where(e => e.NumeroCuenta == numeroCuenta && e.Fecha >= fechaInicio && e.Fecha <= fechaFin)
            .OrderBy(e => e.Fecha)
            .Select(e => new EstadoCuenta_MovimientoDto(
                e.Fecha,
                TipoMovimientoMap.ToCode(e.Tipo),
                e.Valor,
                e.SaldoPosterior))
            .ToListAsync();
    }

    public async Task<decimal> GetSumaHastaAsync(DateTime fechaCorte, int numeroCuenta)
    {
        return await _context.Set<Movimiento>()
            .Where(e => e.NumeroCuenta == numeroCuenta && e.Fecha < fechaCorte)
            .SumAsync(e => (decimal?)e.Valor) ?? 0m;
    }

    public async Task<decimal?> GetLastSaldoPosteriorBeforeAsync(DateTime fechaCorte, int numeroCuenta)
    {
        return await _context.Set<Movimiento>()
            .Where(m => m.NumeroCuenta == numeroCuenta && m.Fecha < fechaCorte)
            .OrderByDescending(m => m.Fecha)
            .Select(m => (decimal?)m.SaldoPosterior)
            .FirstOrDefaultAsync();
    }

}
